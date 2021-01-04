using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System;
using YmlParser.Models;

namespace YmlParser.Parser
{
    class XDocYmlParser : IYmlParser
    {
        public IEnumerable<Product> Parse(string filename, string shopId)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            if (shopId == null)
                throw new ArgumentNullException(nameof(shopId));

            if (!File.Exists(filename)) throw new FileNotFoundException(filename);

            var xDocument = XDocument.Parse(File.ReadAllText(filename));
            return from xe in xDocument.Root.Element("shop").Element("offers").Elements("offer")
                   select new Product() { Id = (int)xe.Attribute("id"), Name = (string)xe.Element("name"), ShopId = shopId };
        }
    }
}
