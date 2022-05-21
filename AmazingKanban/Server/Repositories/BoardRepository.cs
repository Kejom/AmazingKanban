using AmazingKanban.Server.Data;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BoardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Board>> GetAll()
        {
            return await _dbContext.Boards.ToListAsync();
        }

        public async Task<List<Board>> GetByOwnerId(string userId)
        {
            return await _dbContext.Boards.Where(b => b.OwnerId == userId).ToListAsync();
        }

        public async Task<Board?> GetById(int id)
        {
            return await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Board>> GetByUserId(string userId)
        {
            return await _dbContext.BoardUserAccesses.Where(a => a.UserId == userId).Include(a => a.Board).Select(a => a.Board).ToListAsync();
        }

        public async Task AddBoard(Board board)
        {
            _dbContext.Boards.Add(board);
            await _dbContext.SaveChangesAsync();
            await AddAccess(new BoardUserAccess
            {
                UserId = board.OwnerId,
                BoardId = board.Id,
                Role = BoardRoles.Admin
            });           
        }

        public async Task UpdateBoard(Board board)
        {
            _dbContext.Boards.Update(board);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBoard(int id)
        {
            var boardToRemove = await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == id);
            if (boardToRemove is null)
                throw new ArgumentException("Board with given Id doesn't exist");

            _dbContext.Boards.Remove(boardToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BoardUserAccess>> GetAccessLevelByUserId(string userId)
        {
            return await _dbContext.BoardUserAccesses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<List<BoardUserAccess>> GetAccessesByBoardId(int boardId)
        {
            return await _dbContext.BoardUserAccesses.Where(a => a.BoardId == boardId).ToListAsync();
        }

        public async Task<BoardUserAccess?> GetUserBoardAccess(string userId, int boardId)
        {
            return await _dbContext.BoardUserAccesses.FirstOrDefaultAsync(a => a.UserId == userId && a.BoardId == boardId);
        }

        public async Task AddAccess(BoardUserAccess access)
        {
            _dbContext.BoardUserAccesses.Add(access);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccess(BoardUserAccess access)
        {
            _dbContext.BoardUserAccesses.Update(access);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccess(BoardUserAccess access)
        {
            var accessToDelete = await _dbContext.BoardUserAccesses.FirstOrDefaultAsync(a => a.Id == access.Id);
            if (accessToDelete is null)
                throw new ArgumentException("Board Access with given id doesn't exist");

            _dbContext.BoardUserAccesses.Remove(accessToDelete);
            await _dbContext.SaveChangesAsync();
        }

    }
}
