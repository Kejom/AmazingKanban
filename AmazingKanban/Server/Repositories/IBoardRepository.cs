using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface IBoardRepository
    {
        Task AddAccess(BoardUserAccess access);
        Task AddBoard(Board board);
        Task DeleteAccess(BoardUserAccess access);
        Task DeleteBoard(int id);
        Task<List<BoardUserAccess>> GetAccessesByBoardId(int boardId);
        Task<List<Board>> GetAll();
        Task<List<BoardUserAccess>> GetBoardsWithAccessLevelByUserId(string userId);
        Task<Board?> GetById(int id);
        Task<List<Board>> GetByOwnerId(string userId);
        Task<List<Board?>> GetByUserId(string userId);
        Task<BoardUserAccess?> GetUserBoardAccess(string userId, int boardId);
        Task UpdateAccess(BoardUserAccess access);
        Task UpdateBoard(Board board);
    }
}