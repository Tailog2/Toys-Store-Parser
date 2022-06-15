using AngleSharp.Dom;
using Services.Exceptions;
using Services.Factories;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStoreParserLibrary.Collectors;
using ToyStoreParserLibrary.Helpers;

namespace ToyStoreParserLibrary.Processers
{
    internal class WebsiteProcesser
    {
        private readonly ProductsPageProccesser _productsPageProccesser;

        public WebsiteProcesser()
        {
            _productsPageProccesser = new ProductsPageProccesser();
        }

        public async Task<IEnumerable<Toy>> ParseWebsite(string baseUrl, RegionEnum region)
        {
            string? nextPageUrl = baseUrl;
            var pages = new List<Task<IEnumerable<Toy>>>();

            // Parse until there are no pages to load
            // That is nextPageUrl is null or loaded page is null
            do
            {
                Console.WriteLine(nextPageUrl + Environment.NewLine);

                var document = await ToyWebsiteHttpHelper.LoadDocument(region, nextPageUrl);
                if (document == null)
                    break;

                // Give responsibility to parse the current page to a task and then move to the next page 
                var website = WebsiteFactory.CreateWebsite(document, new Url(baseUrl), region);

                Task<IEnumerable<Toy>> items = _productsPageProccesser.GetToysAsync(website);
                // Add tasks to list
                pages.Add(items);

                nextPageUrl = GetNextPageLinkOrDefault(website);
            }
            while (nextPageUrl != null);

            var pagesResult = await Task.WhenAll(pages);
            return pagesResult.SelectMany(page => page);
        }

        private string? GetNextPageLinkOrDefault(Website website)
        {
            string nextPageUrl;

            try
            {
                PaggingCollector paggingCollector = new PaggingCollector();
                nextPageUrl = paggingCollector.GetNextPageUrl(website);
            }
            catch (TimeoutException)
            {
                throw new DownloadPageException();
            }
            catch (Exception)
            {
                return null;
            }

            return nextPageUrl;
        }
    }
}
