using AmazingKanban.Client.Utility;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Blazored.Toast.Services;

namespace AmazingKanban.Client.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRestApiClient _restApiClient;
        private readonly IToastService _toastService;

        public TaskService(IRestApiClient restApiClient, IToastService toastService)
        {
            _restApiClient = restApiClient;
            _toastService = toastService;
        }

        public async Task<List<KanbanTask<UserLite>>> GetByBoardId(int boardId)
        {
            try
            {
                var result = await _restApiClient.GetAsync<List<KanbanTask<UserLite>>>($"api/tasks/board/{boardId}");
                if (result is not null)
                    return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);

            }
            return new List<KanbanTask<UserLite>>();
        }

        public async Task<List<KanbanTask<UserLite>>> GetByBoardIdAndState(int boardId, KanbanTaskStates state)
        {
            try
            {
                var result = await _restApiClient.GetAsync<List<KanbanTask<UserLite>>>($"api/tasks/board/{boardId}/state/{state}");

                if (result is not null)
                    return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }

            return new List<KanbanTask<UserLite>>();
        }

        public async Task<int> Add(KanbanTask<UserLite> task)
        {
            try
            {
                var result = await _restApiClient.PostAsync<int, KanbanTask<UserLite>>("api/tasks", task);
                _toastService.ShowSuccess("Task Created!");
                return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return -1;
            }
        }
    }
}
