using AmazingKanban.Shared.Models;
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


        public async Task<IEnumerable<UserLite>> GetUsers(string filter)
        {
            var url = "api/users";
            if (!String.IsNullOrEmpty(filter))
                url += $"?filter={filter}";

            var result = await _httpClient.GetFromJsonAsync<List<UserLite>>(url);

            if (result is null)
                result = new List<UserLite>();

            return result;
        }

        public async Task<List<BoardUserVM>> GetByBoardId(int boardId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<BoardUserVM>>($"api/users/{boardId}");

            if (result is null)
                result = new List<BoardUserVM>();

            return result;
        }


    }
}
