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

        public async Task<ApplicationUser> GetUserByid(string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new ArgumentException("User with given Id doesnt exist");

            return user;
        }

        public async Task<List<ApplicationUser>> GetByBoardIdAndRole(int boardId, BoardRoles role, string filter = "")
        {
            var result = _dbContext.BoardAccesses.Where(u => u.BoardId == boardId && u.Role >= role).Include(u => u.User).Select(u => u.User!);

            if (!String.IsNullOrEmpty(filter))
                result = result.Where(u => u.Email.ToLower().Contains(filter.ToLower()) ||
                                u.FirstName.ToLower().Contains(filter.ToLower()) ||
                                u.LastName.ToLower().Contains(filter.ToLower()));

            return await result.ToListAsync();
        }
    }
}
