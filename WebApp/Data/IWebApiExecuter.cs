
namespace WebApp.Data
{
    public interface IWebApiExecuter
    {
        Task<T?> InvokeGet<T>(string relativeURL);
        Task<T?> InvokePost<T>(string relativeUrl, T obj);
    }
}