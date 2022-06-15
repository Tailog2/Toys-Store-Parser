using Services.Interfaces.IParsers;
using Services.Models;

namespace Services
{
    public interface IParserService
    {
        IToyParser<Toy> ToyParser { get; }
    }
}