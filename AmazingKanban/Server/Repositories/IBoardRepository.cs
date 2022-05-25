using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface IBoardRepository
    {
        Task AddBoard(Board board);
        Task DeleteBoard(int id);
        Task<List<Board>> GetAll();
        Task<Board> GetById(int id);
        Task<List<Board>> GetByOwnerId(string userId);
        Task<List<Board>> GetByUserId(string userId);
        Task UpdateBoard(Board board);
    }
}