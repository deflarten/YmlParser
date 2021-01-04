using System.Collections.Generic;
using YmlParser.Models;

namespace YmlParser.Repo
{
    public interface IRepository
    {
        public int AddProducts(IEnumerable<Product> products);
        public IEnumerable<Product> GetProducts(string shopId);
    }
}
