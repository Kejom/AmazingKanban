
namespace AmazingKanban.Client.Utility
{
    public interface IRestApiClient
    {
        Task<TReturn?> GetAsync<TReturn>(string url);
        Task<TReturn?> PostAsync<TReturn, TRequest>(string url, TRequest requestBody);
        Task PutAsync<TRequest>(string url, TRequest requestBody);
    }
}