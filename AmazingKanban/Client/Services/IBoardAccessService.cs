using AmazingKanban.Shared.Models;

namespace AmazingKanban.Client.Services
{
    public interface IBoardAccessService
    {
        Task<List<BoardAccess<UserLite>>> GetByBoardId(int boardId);
        Task UpdateForBoardId(int boardId, List<BoardAccess<UserLite>> accesses);
    }
}