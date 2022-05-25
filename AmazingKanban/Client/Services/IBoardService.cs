using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IBoardService
    {
        IList<Board> Boards { get; set; }
        event Action OnChange;
        Task AddBoard(BoardSubmitVM board);
        Task LoadBoardsAsync();
        Task<BoardSubmitVM?> GetBoardById(int id);
        Task UpdateBoardAccesses(int boardId, List<BoardAccess<UserLite>> accesses);
    }
}