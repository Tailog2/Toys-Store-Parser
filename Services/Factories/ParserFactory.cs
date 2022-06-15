using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factories
{
    internal class ParserFactory
    {
        public static TParser CreateParser<TParser>(string root, string libraryName)
        {
            Assembly assembly = Assembly.LoadFrom(root + @"/Parsers/" + libraryName + ".dll");

            var types = assembly.GetTypes();

            var type = types
                .Where(t => t.IsAssignableTo(typeof(TParser)))
                .SingleOrDefault();

            TParser parser = (TParser)Activator.CreateInstance(type);
            return parser;
        }
    }
}
