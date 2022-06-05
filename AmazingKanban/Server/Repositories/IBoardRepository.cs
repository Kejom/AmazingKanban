using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface IBoardRepository
    {
        Task Add(Board board);
        Task Delete(int id);
        Task<List<Board>> GetAll();
        Task<Board> GetById(int id);
        Task<List<Board>> GetByOwnerId(string userId);
        Task<List<Board>> GetByUserId(string userId);
        Task Update(Board board);
        Task<int> GetCount();
    }
}