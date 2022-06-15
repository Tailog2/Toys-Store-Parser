using AngleSharp.Dom;
using Services.Models;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Exceptions;
using Services.Factories;
using Services.Interfaces.IParsers;
using ToyStoreParserLibrary.Processers;

namespace ToyStoreParserLibrary
{
    public class ToyParserService : IToyParser<Toy>
    {
        private RegionEnum _region = default;
        private readonly WebsiteProcesser _websiteProcesser = new WebsiteProcesser();

        public ToyParserService()
        {
        }
        public ToyParserService(RegionEnum region)
        {
            _region = region;
        }

        public RegionEnum Region { set => _region = value; }

        // This method controll the whole parsing process
        public async Task<IEnumerable<Toy>> ParseToEndAsync(string baseUrl)
        {
            return await _websiteProcesser.ParseWebsite(baseUrl, _region);
        }     
    }
}
