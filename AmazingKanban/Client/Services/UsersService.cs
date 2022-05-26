using AmazingKanban.Client.Utility;
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
        private readonly IRestApiClient _restClient;

        public UsersService(HttpClient httpClient, IToastService toastService, IRestApiClient restClient)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _restClient = restClient;
        }


        public async Task<IEnumerable<UserLite>> GetUsers(string filter)
        {
            var url = "api/users";

            if (!String.IsNullOrEmpty(filter))
                url += $"?filter={filter}";

            try
            {
                return await _restClient.GetAsync<IEnumerable<UserLite>>(url);
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return new List<UserLite>();
            }
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
