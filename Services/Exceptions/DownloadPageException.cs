using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class DownloadPageException : Exception
    {
        public DownloadPageException()
        {
        }

        public DownloadPageException(string? message) : base(message)
        {

        }
    }
}
