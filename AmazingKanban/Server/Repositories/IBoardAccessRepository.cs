using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Repositories
{
    public interface IBoardAccessRepository
    {
        Task Add(BoardAccess<ApplicationUser> boardAccess);
        Task Delete(int accessId);
        Task<List<BoardAccess<ApplicationUser>>> GetAll();
        Task<BoardAccess<ApplicationUser>> GetById(int id);
        Task<List<BoardAccess<ApplicationUser>>> GetByBoardId(int boardId);
        Task<List<BoardAccess<ApplicationUser>>> GetByUserId(string userId);
        Task<BoardAccess<ApplicationUser>> GetByUserAndBoardId(string userId, int boardId);
        Task Update(BoardAccess<ApplicationUser> boardAccess);
    }
}