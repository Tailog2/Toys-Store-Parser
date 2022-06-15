using Services.Factories;
using Services.Interfaces.IParsers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ParserService : IParserService
    {
        private readonly IToyParser<Toy> _toyParser;

        private string rootPath = Directory.GetCurrentDirectory();
        private const string toyLibraryName = "ToyStoreParserLibrary";     

        public ParserService()
        {
            _toyParser = ParserFactory.CreateParser<IToyParser<Toy>>(rootPath, toyLibraryName);
        }

        public IToyParser<Toy> ToyParser { get => _toyParser; }   
    }
}
