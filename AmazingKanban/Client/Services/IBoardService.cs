using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IBoardService
    {
        IList<Board> Boards { get; set; }

        event Action OnChange;

        Task AddBoard(BoardSubmitVM boardVM);
        Task<Board?> GetBoardById(int id);
        Task LoadBoardsAsync();
        Task UpdateBoardAccesses(int boardId, List<BoardAccess<UserLite>> accesses);
    }
}