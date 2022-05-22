using AmazingKanban.Shared.ViewModels;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace AmazingKanban.Client.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        public UsersService(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }


        public async Task<IEnumerable<UserVM>> GetUsers(string filter)
        {
            var url = "api/users";
            if (!String.IsNullOrEmpty(filter))
                url += $"?filter={filter}";

            var result = await _httpClient.GetFromJsonAsync<List<UserVM>>(url);

            if (result is null)
                result = new List<UserVM>();

            return result;
        }

        public async Task<List<UserVM>> GetByBoardId(int boardId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserVM>>($"api/users/{boardId}");

            if (result is null)
                result = new List<UserVM>();

            return result;
        }


    }
}
