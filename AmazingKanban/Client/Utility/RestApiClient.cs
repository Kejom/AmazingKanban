using System.Net.Http.Json;

namespace AmazingKanban.Client.Utility
{
    public class RestApiClient : IRestApiClient
    {
        protected readonly HttpClient _httpClient;

        public RestApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TReturn?> GetAsync<TReturn>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TReturn>();

            string msg = await response.Content.ReadAsStringAsync();
            throw new Exception(msg);
        }

        public async Task<TReturn?> PostAsync<TReturn, TRequest>(string url, TRequest requestBody)
        {
            var response = await _httpClient.PostAsJsonAsync<TRequest>(url, requestBody);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TReturn>();

            string msg = await response.Content.ReadAsStringAsync();
            throw new Exception(msg);
        }

        public async Task PutAsync<TRequest>(string url, TRequest requestBody)
        {
            var response = await _httpClient.PutAsJsonAsync<TRequest>(url, requestBody);

            if (response.IsSuccessStatusCode)
                return;

            string msg = await response.Content.ReadAsStringAsync();
            throw new Exception(msg);
        }

        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
                return;

            string msg = await response.Content.ReadAsStringAsync();
            throw new Exception(msg);
        }
    }
}
