using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserLite>> GetUsers(string filter);
        Task<List<UserVM>> GetAllAsVM();
        Task<UserLite?> GetById(string userId);
        Task<bool> PromoteToAdmin(string userId);
        Task<bool> DemoteAdmin(string userId);
        Task<bool> Lock(string userId);
        Task<bool> Unlock(string userId);
        Task<IEnumerable<UserLite>> GetByBoardIdAndRole(int boardId, BoardRoles role, string filter);
    }
}