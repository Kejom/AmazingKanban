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
            var result = await _httpClient.GetFromJsonAsync<IList<Board>>("api/board/getBoards");

            if (result is not null)
                Boards = result;
        }

        public async Task AddBoard(Board board)
        {
            var result = await _httpClient.PostAsJsonAsync("api/board/add", board);

            if(result.StatusCode != System.Net.HttpStatusCode.OK)
                _toastService.ShowError(await result.Content.ReadAsStringAsync());

            var addedBoard = await result.Content.ReadFromJsonAsync<Board>();
            Boards.Add(addedBoard);
            BoardChanged();
            _toastService.ShowSuccess($"Board {addedBoard.Name} has been created.");
        }

        void BoardChanged() => OnChange.Invoke();
    }
}
