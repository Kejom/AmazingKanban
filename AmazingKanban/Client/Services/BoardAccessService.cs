using AmazingKanban.Client.Utility;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Blazored.Toast.Services;

namespace AmazingKanban.Client.Services
{
    public class BoardAccessService : IBoardAccessService
    {
        private readonly IRestApiClient _restClient;
        private readonly IToastService _toastService;

        public BoardAccessService(IRestApiClient restClient, IToastService toastService)
        {
            _restClient = restClient;
            _toastService = toastService;
        }

        public async Task<List<BoardAccess<UserLite>>> GetByBoardId(int boardId)
        {
            try
            {
                var result = await _restClient.GetAsync<List<BoardAccess<UserLite>>>($"api/boards/access/board/{boardId}");

                if (result is not null)
                    return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
            return new List<BoardAccess<UserLite>>();
        }

        public async Task<BoardAccess<UserLite>>GetByBoardAndUserId(int boardId, string userId)
        {
            try
            {
                var result = await _restClient.GetAsync<BoardAccess<UserLite>>($"api/boards/access/board/{boardId}/user/{userId}");

                if (result is not null)
                    return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
            return new BoardAccess<UserLite> { Role = BoardRoles.NoAccess };
        }

        public async Task UpdateForBoardId(int boardId, List<BoardAccess<UserLite>> accesses)
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

        public async Task<int> Add(BoardAccess<UserLite> access)
        {
            try
            {
                var result = await _restClient.PostAsync<int, BoardAccess<UserLite>>("api/boards/access", access);
                _toastService.ShowSuccess($"Access added");
                return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return -1;
            }
        }

        public async Task<bool> Update (BoardAccess<UserLite> access)
        {
            try
            {
                await _restClient.PutAsync<BoardAccess<UserLite>>("api/boards/access", access);
                _toastService.ShowSuccess($"Access updated");
                return true;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete (BoardAccess<UserLite> access)
        {
            try
            {
                await _restClient.DeleteAsync($"api/boards/access/{access.Id}");
                _toastService.ShowSuccess($"Access deleted");
                return true;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return false;
            }
        }
    }
}
