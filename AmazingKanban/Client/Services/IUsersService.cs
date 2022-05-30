using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserLite>> GetUsers(string filter);
        Task<UserLite?> GetById(string userId);
        Task<IEnumerable<UserLite>> GetByBoardIdAndRole(int boardId, BoardRoles role, string filter);
    }
}