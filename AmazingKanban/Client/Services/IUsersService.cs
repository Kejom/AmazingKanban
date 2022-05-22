using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserVM>> GetUsers(string filter);
        Task<List<UserVM>> GetByBoardId(int boardId);
    }
}