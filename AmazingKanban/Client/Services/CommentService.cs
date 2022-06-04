using AmazingKanban.Client.Utility;
using AmazingKanban.Shared.Models;
using Blazored.Toast.Services;

namespace AmazingKanban.Client.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRestApiClient _restApiClient;
        private readonly IToastService _toastService;

        public CommentService(IRestApiClient restApiClient, IToastService toastService)
        {
            _restApiClient = restApiClient;
            _toastService = toastService;
        }

        public async Task<List<TaskComment<UserLite>>> GetByTaskId(int taskId)
        {
            try
            {
                var result = await _restApiClient.GetAsync<List<TaskComment<UserLite>>>($"api/comments/task/{taskId}");
                if (result is not null)
                    return result;
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }

            return new List<TaskComment<UserLite>>();
        }

        public async Task<int> AddComment(TaskComment<UserLite> comment)
        {
            try
            {
                return await _restApiClient.PostAsync<int, TaskComment<UserLite>>("api/comments", comment);
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return -1;
            }
        }


    }
}
