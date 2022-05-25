using AmazingKanban.Server.Data;
using AmazingKanban.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskCommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskComment<ApplicationUser>>> GetAll()
        {
            return await _dbContext.TaskComments.Include(t => t.CreatedBy).ToListAsync();
        }

        public async Task<TaskComment<ApplicationUser>> GetById(int id)
        {
            var result = await _dbContext.TaskComments.FirstOrDefaultAsync(c => c.Id == id);

            if (result is null)
                throw new ArgumentException("Comment with given Id doesnt exist");

            return result;
        }

        public async Task<List<TaskComment<ApplicationUser>>> GetByBoardId(int boardId)
        {
            return await _dbContext.TaskComments.Where(c => c.BoardId == boardId).Include(t => t.CreatedBy).ToListAsync();
        }

        public async Task<List<TaskComment<ApplicationUser>>> GetByTaskId(int taskId)
        {
            return await _dbContext.TaskComments.Where(c => c.TaskId == taskId).Include(t => t.CreatedBy).ToListAsync();
        }

        public async Task<List<TaskComment<ApplicationUser>>> GetByCreatedById(string createdById)
        {
            return await _dbContext.TaskComments.Where(c => c.CreatedById == createdById).ToListAsync();
        }

        public async Task Add(TaskComment<ApplicationUser> comment)
        {
            await _dbContext.TaskComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TaskComment<ApplicationUser> comment)
        {
            _dbContext.TaskComments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int commentId)
        {
            var commentToRemove = await _dbContext.TaskComments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (commentToRemove is null)
                throw new ArgumentException("Comment with given Id doesnt exist");

            _dbContext.TaskComments.Remove(commentToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}
