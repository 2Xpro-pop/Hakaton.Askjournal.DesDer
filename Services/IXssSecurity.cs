namespace DesDer.Services
{
    public interface IXssSecurity
    {
        bool CheckPost(string rawHtml);
        bool CheckUrl(string url);
    }
}