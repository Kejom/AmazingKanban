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
        public event Action OnChange;

        public TaskService(IRestApiClient restApiClient, IToastService toastService)
        {
            _restApiClient = restApiClient;
            _toastService = toastService;
            States = Enum.GetValues(typeof(KanbanTaskStates)).Cast<KanbanTaskStates>();
            InitializeTaskDict();
        }

        public IDictionary<KanbanTaskStates, IList<KanbanTask<UserLite>>> Tasks { get; set; }
        public IEnumerable<KanbanTaskStates> States { get; set; }

        public async Task LoadTasksAsync(int boardId)
        {
            InitializeTaskDict();
            foreach (var state in States)
            {
                Tasks[state] = await GetByBoardIdAndState(boardId, state);
            }
            TasksChanged();
        }
        public async Task<KanbanTask<UserLite>?>GetById(int taskId)
        {
            try
            {
               return await _restApiClient.GetAsync<KanbanTask<UserLite>>($"api/tasks/{taskId}");
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
                return null;
            }
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

        public async Task Add(KanbanTask<UserLite> task)
        {
            try
            {
                var taskId = await _restApiClient.PostAsync<int, KanbanTask<UserLite>>("api/tasks", task);
                _toastService.ShowSuccess("Task Created!");
                task.Id = taskId;
                Tasks[task.State].Add(task);
                TasksChanged();

            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);

            }
        }

        public async Task Update(KanbanTask<UserLite> task)
        {
            try
            {
                await _restApiClient.PutAsync<KanbanTask<UserLite>>("api/tasks", task);
                _toastService.ShowSuccess("Task Updated!");
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message);
            }
        }

        void TasksChanged() => OnChange.Invoke();

        private void InitializeTaskDict()
        {
            Tasks = new Dictionary<KanbanTaskStates, IList<KanbanTask<UserLite>>>();
            foreach (var state in States)
            {
                Tasks.Add(state, new List<KanbanTask<UserLite>>());
            }
        }
    }
}
