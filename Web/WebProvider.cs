using System.Net;
using YmlParser.Web;

namespace YmlParser
{
    class WebProvider : IWebProvider
    {
        public void DownloadFile(string url, string filename)
        {
            using var wc = new WebClient();
            wc.DownloadFile(url, filename);
        }
    }
}
