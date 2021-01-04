using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YmlParser.Models;

namespace YmlParser.DataExport
{
    public class CsvDataExport : IDataExport
    {
        public string GetString(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            if (!products.Any())
                return String.Empty;

            var sb = new StringBuilder();
            sb.AppendLine(string.Join(";", new string[] { "id", "name" }));

            foreach (Product product in products)
                sb.AppendLine(string.Join(";", new string[] { product.Id.ToString(), product.Name }));

            return sb.ToString();
        }
    }
}
