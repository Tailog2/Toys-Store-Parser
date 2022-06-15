using Services.Models;

namespace Services.Interfaces.IParsers
{
    public interface IToyParser<TModel> : IParser<TModel>, ICanChangeRegion where TModel : class
    {

    }
}