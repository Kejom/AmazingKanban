using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace AmazingKanban.Client.Services
{
    public class BoardService : IBoardService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        public event Action OnChange;

        public BoardService(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }
        public IList<Board> Boards { get; set; } = new List<Board>();

        public async Task LoadBoardsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IList<Board>>("api/boards");

            if (result is not null)
                Boards = result;
        }
        public async Task<BoardVM?> GetBoardById(int id)
        {
            return await _httpClient.GetFromJsonAsync<BoardVM>($"api/boards/{id}");
        }

        public async Task AddBoard(BoardVM boardVM)
        {
            var result = await _httpClient.PostAsJsonAsync("api/boards/add", boardVM);

            if(result.StatusCode != System.Net.HttpStatusCode.OK)
                _toastService.ShowError(await result.Content.ReadAsStringAsync());

            var addedBoard = await result.Content.ReadFromJsonAsync<Board>();
            Boards.Add(addedBoard);
            BoardChanged();
            _toastService.ShowSuccess($"Board {addedBoard.Name} has been created.");
        }

        public async Task UpdateBoardAccesses(int boardId, List<BoardUserAccess> accesses)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/boards/access/{boardId}", accesses);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            else
                _toastService.ShowSuccess("Accesses Updated");
        }

        void BoardChanged() => OnChange.Invoke();
    }
}
