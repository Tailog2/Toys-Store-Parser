using AngleSharp.Dom;
using Services.Factories;
using Services.Helpers;
using Services.Interfaces;
using Services.Models;
using ToyStoreParserLibrary.Collectors;
using ToyStoreParserLibrary.Helpers;

namespace ToyStoreParserLibrary.Processers
{
    internal class ProductsPageProccesser
    {
        private readonly ProductsLinksColllector _productsLinksColllector;
        private readonly DetailsPageProcesser _detailsPageProcesser;

        public ProductsPageProccesser()
        {
            _productsLinksColllector = new ProductsLinksColllector();
            _detailsPageProcesser = new DetailsPageProcesser();
        }

        public async Task<IEnumerable<Toy>> GetToysAsync(Website website)
        {
            var links = await _productsLinksColllector.GetItemsLinksAsync(website.Document);

            return await ParseProductsAsync(links, website.Region);
        }

        private async Task<IEnumerable<Toy>> ParseProductsAsync(IEnumerable<string> links, RegionEnum region)
        {
            List<Task<Toy?>> tasks = new List<Task<Toy?>>();

            foreach (var url in links)
            {
                Task<Toy?> task = ParseProductAsync(region, url);
                tasks.Add(task);
            }

            var items = await Task.WhenAll(tasks);

            return items.Where(i => i != null);
        }

        private async Task<Toy?> ParseProductAsync(RegionEnum region, string url)
        {

            var document = await ToyWebsiteHttpHelper.LoadDocument(region, url);

            if (document == null)
                return null;

            var website = WebsiteFactory.CreateWebsite(document, new Url(url));

            return await _detailsPageProcesser.GetToyDetailsAsync(website);
        }
    }
}
