using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using System.Net.Http.Json;

namespace AmazingKanban.Client.Services
{
    public class BoardService : IBoardService
    {
        private readonly HttpClient _httpClient;

        public BoardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            var addedBoard = await result.Content.ReadFromJsonAsync<Board>();
            Boards.Add(addedBoard);           
        }
    }
}
