using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;

namespace AmazingKanban.Client.Services
{
    public interface ITaskService
    {
        Task<int> Add(KanbanTask<UserLite> task);
        Task<List<KanbanTask<UserLite>>> GetByBoardId(int boardId);
        Task<List<KanbanTask<UserLite>>> GetByBoardIdAndState(int boardId, KanbanTaskStates state);
    }
}