using AngleSharp.Dom;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factories
{
    public class WebsiteFactory
    {
        public static Website CreateWebsite(IDocument document, Url url, RegionEnum region)
        {
            return new Website()
            {
                Document = document,
                Url = url,
                Region = region
            };
        }

        public static Website CreateWebsite(IDocument document, Url url)
        {
            return new Website()
            {
                Document = document,
                Url = url
            };
        }
    }
}
