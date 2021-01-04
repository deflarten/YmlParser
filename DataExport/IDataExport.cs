using System.Collections.Generic;
using YmlParser.Models;

namespace YmlParser.DataExport
{
    public interface IDataExport
    {
        public string GetString(IEnumerable<Product> products);
    }
}
