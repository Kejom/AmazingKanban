using AmazingKanban.Shared.Models;
using AmazingKanban.Shared.ViewModels;

namespace AmazingKanban.Client.Services
{
    public interface IBoardService
    {
        IList<Board> Boards { get; set; }

        Task AddBoard(Board board);
        Task LoadBoardsAsync();
    }
}