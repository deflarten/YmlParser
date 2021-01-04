using System.Collections.Generic;
using YmlParser.Models;

namespace YmlParser.Parser
{
    public interface IYmlParser
    {
        public IEnumerable<Product> Parse(string filename, string shopId);
    }
}
