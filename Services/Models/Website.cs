using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Services.Models
{
    public class Website
    {
        public Url Url { get; set; }
        public IDocument Document { get; set; }
        public RegionEnum Region { get; set; } = RegionEnum.Undefined;
    }
}
