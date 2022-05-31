using AmazingKanban.Server.Data;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Repositories
{
    public class KanbanTaskRepository : IKanbanTaskRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public KanbanTaskRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetAll()
        {
            return await _dbcontext.KanbanTasks
                .Include(t=> t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task<KanbanTask<ApplicationUser>> GetById(int id)
        {
            var result = await _dbcontext.KanbanTasks.Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (result is null)
                throw new ArgumentException("Task with given Id doesnt exist");

            return result;
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetByBoardId(int boardId)
        {
            return await _dbcontext.KanbanTasks
                .Where(t => t.BoardId == boardId)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetByBoardIdAndState(int boardId, KanbanTaskStates state)
        {
            return await _dbcontext.KanbanTasks
                .Where(t => t.BoardId == boardId && t.State == state)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetByCreatedById(string createdById)
        {
            return await _dbcontext.KanbanTasks
                .Where(t => t.CreatedById == createdById)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetByAssignedToId(string assignedToId)
        {
            return await _dbcontext.KanbanTasks
                .Where(t => t.AssignedToId == assignedToId)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task<List<KanbanTask<ApplicationUser>>> GetByValidatorId(string validatorId)
        {
            return await _dbcontext.KanbanTasks
                .Where(t => t.ValidatorId == validatorId)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Validator)
                .ToListAsync();
        }

        public async Task Add(KanbanTask<ApplicationUser> task)
        {
            await _dbcontext.KanbanTasks.AddAsync(task);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Update(KanbanTask<ApplicationUser> task)
        {
            _dbcontext.KanbanTasks.Update(task);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Delete(int TaskId)
        {
            var taskToRemove = await _dbcontext.KanbanTasks.FirstOrDefaultAsync(t => t.Id == TaskId);

            if (taskToRemove is null)
                throw new ArgumentException("Task with given id doesnt exist");

            _dbcontext.KanbanTasks.Remove(taskToRemove);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
