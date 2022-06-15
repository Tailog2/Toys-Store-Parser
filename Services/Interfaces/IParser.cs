using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IParser<T> where T : class
    {
        public Task<IEnumerable<T>> ParseToEndAsync(string baseUrl);
    }
}
