using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using YmlParser.Models;

namespace YmlParser.Parser
{
    public class StreamParser : IYmlParser
    {
        public IEnumerable<Product> Parse(string filename, string shopId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using Stream stream = File.OpenRead(filename);
            using XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse, MaxCharactersFromEntities = 1024 });
            reader.ReadToFollowing("offer");

            do
            {
                reader.MoveToAttribute("id");
                int id = Int32.Parse(reader.Value);

                reader.ReadToFollowing("name");
                string name = reader.ReadElementContentAsString();

                yield return new Product() { Id = id, Name = name, ShopId = shopId };

            } while (reader.ReadToFollowing("offer"));
        }
    }
}
