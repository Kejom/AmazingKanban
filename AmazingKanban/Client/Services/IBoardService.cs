using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IBoardService
    {
        IList<Board> Boards { get; set; }
        event Action OnChange;
        Task AddBoard(BoardVM board);
        Task LoadBoardsAsync();
        Task<BoardVM?> GetBoardById(int id);
        Task UpdateBoardAccesses(int boardId, List<BoardUserAccess> accesses);
    }
}