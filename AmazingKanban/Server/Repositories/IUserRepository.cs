using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Server.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserVM>> GetUsersAsVM(string filter = "");
        Task<List<UserVM>> GetUsersAsVmByBoardId(int boardId);
    }
}