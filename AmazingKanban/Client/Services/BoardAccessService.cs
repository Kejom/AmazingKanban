using AmazingKanban.Client.Utility;
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
    }
}
