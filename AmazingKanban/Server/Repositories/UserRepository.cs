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

        public async Task<List<UserVM>> GetUsersAsVM(string filter = "")
        {
            if (String.IsNullOrEmpty(filter))
                return await _dbContext.Users.Select(u => ConvertToVM(u)).ToListAsync();

            return await _dbContext.Users
                .Where(u => u.Email.ToLower().Contains(filter.ToLower()) ||
                u.FirstName.ToLower().Contains(filter.ToLower()) ||
                u.LastName.ToLower().Contains(filter.ToLower()))
                .Select(u => ConvertToVM(u))
                .ToListAsync();
        }
        public async Task<List<UserVM>> GetUsersAsVmByBoardId(int boardId)
        {
            return await _dbContext.BoardUserAccesses
                .Where(a=> a.BoardId == boardId)
                .Include(a => a.User)
                .Select(a => ConvertToVM(a))
                .ToListAsync();
        }

        private static bool CheckUser(ApplicationUser user, string filter)
        {
            if (user.Email.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        private static UserVM ConvertToVM(ApplicationUser user)
        {
            return new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        private static UserVM ConvertToVM(BoardUserAccess userAccess)
        {
            return new UserVM
            {
                Id = userAccess.User.Id,
                Email = userAccess.User.Email,
                FirstName = userAccess.User.FirstName,
                LastName = userAccess.User.LastName,
                BoardRole = userAccess.Role,
            };
        }
    }
}
