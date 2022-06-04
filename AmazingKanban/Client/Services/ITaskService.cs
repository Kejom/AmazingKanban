using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;

namespace AmazingKanban.Client.Services
{
    public interface ITaskService
    {
        event Action OnChange;
        IDictionary<KanbanTaskStates, IList<KanbanTask<UserLite>>> Tasks { get; set; }
        IEnumerable<KanbanTaskStates> States { get; set; }
        Task LoadTasksAsync(int boardId);
        Task<KanbanTask<UserLite>?> GetById(int taskId);
        Task Add(KanbanTask<UserLite> task);
        Task Update(KanbanTask<UserLite> task);
        Task<List<KanbanTask<UserLite>>> GetByBoardId(int boardId);
        Task<List<KanbanTask<UserLite>>> GetByBoardIdAndState(int boardId, KanbanTaskStates state);
    }
}