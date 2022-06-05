using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface IKanbanTaskRepository
    {
        Task Add(KanbanTask<ApplicationUser> task);
        Task Delete(int TaskId);
        Task<List<KanbanTask<ApplicationUser>>> GetAll();
        Task<KanbanTask<ApplicationUser>> GetById(int id);
        Task<List<KanbanTask<ApplicationUser>>> GetByAssignedToId(string assignedToId);
        Task<List<KanbanTask<ApplicationUser>>> GetByBoardId(int boardId);
        Task<List<KanbanTask<ApplicationUser>>> GetByBoardIdAndState(int boardId, KanbanTaskStates state);
        Task<List<KanbanTask<ApplicationUser>>> GetByCreatedById(string createdById);
        Task<List<KanbanTask<ApplicationUser>>> GetByValidatorId(string validatorId);
        Task Update(KanbanTask<ApplicationUser> task);
        Task<int> GetCount();
    }
}