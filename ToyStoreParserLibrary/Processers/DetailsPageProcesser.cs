using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Services.Exceptions;
using Services.Models;
using ToyStoreParserLibrary.Collectors;

#nullable disable

namespace ToyStoreParserLibrary.Processers
{
    internal class DetailsPageProcesser
    {
        public async Task<Toy> GetToyDetailsAsync(Website website)
        {
            var itemHelper = new DetailsCollector(website.Document);
            Toy item = new Toy();

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => item.Name = itemHelper.GetName()));
            tasks.Add(Task.Run(() => item.Price = itemHelper.GetPrice()));
            tasks.Add(Task.Run(() => item.OldPrice = itemHelper.GetOldPrice()));
            tasks.Add(Task.Run(() => item.ProductUrl = website.Url.ToString()));
            tasks.Add(Task.Run(() => item.SmallPictuesUrls = itemHelper.GetSmallPictues()));
            tasks.Add(Task.Run(() => item.BigPictuesUrls = itemHelper.GetBigPictues()));
            tasks.Add(Task.Run(() => item.Region = itemHelper.GetRegion()));
            tasks.Add(Task.Run(() => item.IsInStock = itemHelper.IsInStock()));
            tasks.Add(Task.Run(() => item.Crumbs = itemHelper.GetCrumbs()));

            await Task.WhenAll(tasks);

            return item;
        }

        public Toy GetToyDetails(Website website)
        {
            var itemHelper = new DetailsCollector(website.Document);

            Toy item = new Toy();

            item.Name = itemHelper.GetName();
            item.Price = itemHelper.GetPrice();
            item.OldPrice = itemHelper.GetOldPrice();
            item.ProductUrl = website.Url.ToString();
            item.SmallPictuesUrls = itemHelper.GetSmallPictues();
            item.BigPictuesUrls = itemHelper.GetBigPictues();
            item.Region = itemHelper.GetRegion();
            item.IsInStock = itemHelper.IsInStock();
            item.Crumbs = itemHelper.GetCrumbs();

            return item;
        }
    }
}
