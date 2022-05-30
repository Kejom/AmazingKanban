using AmazingKanban.Shared.Models;

namespace AmazingKanban.Client.Services
{
    public interface IBoardAccessService
    {
        Task<List<BoardAccess<UserLite>>> GetByBoardId(int boardId);
        Task<BoardAccess<UserLite>> GetByBoardAndUserId(int boardId, string userId);
        Task UpdateForBoardId(int boardId, List<BoardAccess<UserLite>> accesses);
        Task<int> Add(BoardAccess<UserLite> access);
        Task<bool> Update(BoardAccess<UserLite> access);
        Task<bool> Delete(BoardAccess<UserLite> access);
    }
}