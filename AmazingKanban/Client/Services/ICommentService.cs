using AmazingKanban.Shared.Models;

namespace AmazingKanban.Client.Services
{
    public interface ICommentService
    {
        Task<int> AddComment(TaskComment<UserLite> comment);
        Task<List<TaskComment<UserLite>>> GetByTaskId(int taskId);
    }
}