using AmazingKanban.Client.Utility;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Blazored.Toast.Services;
using System.Net.Http.Json;

namespace AmazingKanban.Client.Services
{
    public class BoardService : IBoardService
    {
        private readonly IToastService _toastService;
        private readonly IRestApiClient _restClient;

        public event Action OnChange;

        public BoardService( IToastService toastService, IRestApiClient restClient)
        {
            _toastService = toastService;
            _restClient = restClient;
        }
        public IList<Board> Boards { get; set; } = new List<Board>();

        public async Task LoadBoardsAsync()
        {
            try
            {
                var result = await _restClient.GetAsync<List<Board>>("api/boards");
                if (result is not null)
                    Boards = result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
        }
        public async Task<Board?> GetBoardById(int id)
        {
            try
            {
                return await _restClient.GetAsync<Board>($"api/boards/{id}");
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return null;
            }
        }

        public async Task AddBoard(BoardSubmitVM boardVM)
        {
            try
            {
                var addedBoard = await _restClient.PostAsync<Board, BoardSubmitVM>("api/boards", boardVM);
                Boards.Add(addedBoard);
                BoardChanged();
                _toastService.ShowSuccess($"Board {addedBoard.Name} has been created.");
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
        }

        public async Task UpdateBoardAccesses(int boardId, List<BoardAccess<UserLite>> accesses)
        {
            try
            {
                await _restClient.PutAsync<List<BoardAccess<UserLite>>>($"api/boards/access/board/{boardId}", accesses);
                _toastService.ShowSuccess("Accesses Updated");
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
        }

        void BoardChanged() => OnChange.Invoke();
    }
}
