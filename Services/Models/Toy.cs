using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Toy 
    {
        public string? Name { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? Price { get; set; }
        public string? ProductUrl { get; set; }
        public string? Region { get; set; }
        public IEnumerable<string>? Crumbs { get; set; }    
        public IEnumerable<string>? BigPictuesUrls { get; set; }
        public IEnumerable<string>? SmallPictuesUrls { get; set; }
        public bool IsInStock { get; set; }
    }
}
