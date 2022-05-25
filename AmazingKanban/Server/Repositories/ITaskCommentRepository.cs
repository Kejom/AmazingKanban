using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface ITaskCommentRepository
    {
        Task Add(TaskComment<ApplicationUser> comment);
        Task Delete(int commentId);
        Task<List<TaskComment<ApplicationUser>>> GetAll();
        Task<TaskComment<ApplicationUser>> GetById(int id);
        Task<List<TaskComment<ApplicationUser>>> GetByBoardId(int boardId);
        Task<List<TaskComment<ApplicationUser>>> GetByCreatedById(string createdById);
        Task<List<TaskComment<ApplicationUser>>> GetByTaskId(int taskId);
        Task Update(TaskComment<ApplicationUser> comment);
    }
}