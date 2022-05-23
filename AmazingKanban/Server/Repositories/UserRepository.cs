using AmazingKanban.Server.Data;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ApplicationUser>> GetUsers(string filter = "")
        {
            if (String.IsNullOrEmpty(filter))
                return await _dbContext.Users.ToListAsync();

            return await _dbContext.Users
                .Where(u => u.Email.ToLower().Contains(filter.ToLower()) ||
                u.FirstName.ToLower().Contains(filter.ToLower()) ||
                u.LastName.ToLower().Contains(filter.ToLower()))
                .ToListAsync();
        }
        public async Task<List<BoardUserAccess>> GetUsersByBoardId(int boardId)
        {
            return await _dbContext.BoardUserAccesses
                .Where(a=> a.BoardId == boardId)
                .Include(a => a.User)
                .ToListAsync();
        }
    }
}
