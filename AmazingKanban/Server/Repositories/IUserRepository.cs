using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Server.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers(string filter = "");
        Task<ApplicationUser> GetUserByid(string id);
        Task<List<ApplicationUser>> GetByBoardIdAndRole(int boardId, BoardRoles role, string filter = "");
    }
}