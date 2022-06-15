using AngleSharp.Dom;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace ToyStoreParserLibrary.Collectors
{
    internal class PaggingCollector
    {
        public string GetNextPageUrl(Website website)
        {
            var wrapper = website.Document.GetElementById("wrapper");
            var listOfProducts = wrapper?.GetElementsByClassName("row mt-2").FirstOrDefault();
            var pagesNavbar = listOfProducts.NextElementSibling;
            var pagesList = pagesNavbar.QuerySelector("ul.pagination.justify-content-between");
            var pagesItems = pagesList.QuerySelectorAll("li.page-item");
            var lastItem = pagesItems.Last();

            if (lastItem.ClassName.Contains("active"))
                return null;

            var nextPageUrlPart = lastItem
                .GetElementsByTagName("a")?
                .FirstOrDefault()?
                .GetAttribute("href");

            return new Url(nextPageUrlPart, website.Url.Origin).ToString();
        }
      
    }
}
