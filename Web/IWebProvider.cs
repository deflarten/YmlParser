namespace YmlParser.Web
{
    public interface IWebProvider
    {
        public void DownloadFile(string url, string filename);
    }
}
