using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyStoreParserLibrary.Collectors
{
    internal class DetailsCollector
    {
        private readonly IDocument _document;

        public DetailsCollector(IDocument document)
        {
            _document = document;
        }

        public string? GetName()
        {
            try
            {
                return _document
                .GetElementsByClassName("detail-name")?
                .FirstOrDefault()?
                .GetAttribute("content");
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'Name' property");
            }
        }

        public decimal? GetPrice()
        {
            string? priceString = "";
            try
            {
                priceString = _document
                .GetElementsByClassName("price")?
                .FirstOrDefault()?
                .GetAttribute("content");

                if (String.IsNullOrWhiteSpace(priceString))
                    return null;

                return TextToPrice(priceString);
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'Price' property");
            }
        }

        public decimal? GetOldPrice()
        {
            try
            {
                var priceString = _document
                .GetElementsByClassName("old-price")?
                .FirstOrDefault()?
                .Text();

                if (String.IsNullOrWhiteSpace(priceString))
                    return null;

                return TextToPrice(priceString);
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'OldPries' property");
            }
        }

        public IEnumerable<string?> GetSmallPictues()
        {
            try
            {
                var result = new List<string?>();

                var main = _document.GetElementsByTagName("main")
                    .SingleOrDefault();

                var detailsBlock = main?
                    .QuerySelector("meta[itemprop='gtin14']")?
                    .NextElementSibling;

                var smallCards = detailsBlock?
                    .QuerySelector("div*.card-slider-nav");

                var cards = smallCards?
                    .GetElementsByTagName("div");

                if (cards == null)
                    return result;

                foreach (var imageBlock in cards)
                {
                    var image = imageBlock
                        .QuerySelector("img*.img-fluid");

                    var link = image?.GetAttribute("src");
                    result.Add(link);
                }

                return result;
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'SmallPictues' property");
            }
        }

        public IEnumerable<string?> GetBigPictues()
        {
            try
            {
                var result = new List<string?>();

                var main = _document.GetElementsByTagName("main")
                    .SingleOrDefault();

                var detailsBlock = main?
                    .QuerySelector("meta[itemprop='gtin14']")?
                    .NextElementSibling;

                var bigImagesBlock = detailsBlock?
                    .GetElementsByClassName("detail-image")?
                    .FirstOrDefault();

                var bigImages = bigImagesBlock?
                    .QuerySelector("div*.card-slider-for")?
                    .GetElementsByTagName("div");

                if (bigImages == null)
                    return result;

                foreach (var imageBlock in bigImages)
                {
                    var image = imageBlock
                        .QuerySelector("img*.img-fluid");

                    var link = image?.GetAttribute("src");

                    result.Add(link);
                }

                return result;
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'BigPictues' property");
            }
        }

        public string? GetRegion()
        {
            try
            {
                var header = _document
                .GetElementsByTagName("header")?
                .FirstOrDefault();

                var locationNode = header?
                .GetElementsByClassName("top-location")?
                .FirstOrDefault();

                var rigionLink = locationNode?
                .QuerySelector("a[data-src='#region']");

                return rigionLink?.Text().Trim();
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'Region' property");
            }
        }

        public bool IsInStock()
        {
            var result = false;
            try
            {
                var main = _document.GetElementsByTagName("main")?
                .FirstOrDefault();

                var details = main?
                .GetElementsByClassName("detail-block")?
                .FirstOrDefault();

                var offers = details?
                .QuerySelector("div[itemprop='offers']");

                var okNode = offers?
                .GetElementsByClassName("ok")
                .FirstOrDefault();

                if (okNode == null)
                    return result;

                result = okNode.Children.HasClass("v");

                return result;
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'IsInStock' property");
            }
        }

        public IEnumerable<string?> GetCrumbs()
        {
            try
            {
                var result = new List<string?>();

                var main = _document.GetElementsByTagName("main")?
                    .FirstOrDefault();

                var breadcrumbsList = main?
                    .GetElementsByClassName("breadcrumb")
                    .FirstOrDefault();

                var breadcrumbs = breadcrumbsList?
                    .QuerySelectorAll("a*.breadcrumb-item");

                if (breadcrumbs == null)
                {
                    return result;
                }

                foreach (var breadcrumb in breadcrumbs)
                {
                    result.Add(breadcrumb.GetAttribute("title"));
                }

                var active = breadcrumbsList?
                    .QuerySelector("span*.breadcrumb-item");
                result.Add(active?.Text());

                return result;
            }
            catch (Exception)
            {
                throw new ItemFetchInfoException("An error occurred when the ToyStoreParserLibrary tried get 'IsInStock' property");
            }
        }

        private decimal TextToPrice(string textPrice)
        {
            textPrice.Trim();

            Regex regex = new Regex(@"руб\.");
            string priceWithoutCurrency = regex.Replace(textPrice, String.Empty).Replace(" ", String.Empty);

            return Decimal.Parse(priceWithoutCurrency);
        }

    }
}
