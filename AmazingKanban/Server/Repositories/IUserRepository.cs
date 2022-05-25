using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Server.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers(string filter = "");
    }
}