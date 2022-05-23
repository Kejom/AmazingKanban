using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserLite>> GetUsers(string filter);
        Task<List<BoardUserVM>> GetByBoardId(int boardId);
    }
}