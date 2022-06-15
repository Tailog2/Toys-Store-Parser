using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace ToyStoreParserLibrary.Collectors
{
    internal class ProductsLinksColllector
    {
        public async Task<IEnumerable<string>> GetItemsLinksAsync(IDocument document)
        {
            var wrapper = document.GetElementById("wrapper");
            var listOfProducts = wrapper?.GetElementsByClassName("row mt-2").FirstOrDefault();
            var productsCards = listOfProducts?.GetElementsByClassName("h-100 product-card");

            IEnumerable<Task<string>> tasks = productsCards?
                .Select(c => Task<string>.Factory.StartNew(() =>
                    GetItemUrl(c))
                );

            return await Task.WhenAll(tasks);
        }

        private string GetItemUrl(IElement card)
        {
            return card?
                .QuerySelector(@"meta[itemprop='url']")?
                .GetAttribute("content");
        }
    }
}
