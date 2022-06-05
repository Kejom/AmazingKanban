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

        public async Task<Board> GetById(int id)
        {
            var board = await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == id);

            if (board is null)
                throw new ArgumentException("Board with given Id doesn't exist");

            return board;
        }

        public async Task<List<Board>> GetByUserId(string userId)
        {
            return await _dbContext.BoardAccesses.Where(b => b.UserId == userId).Include(b => b.Board).Select(b => b.Board!).ToListAsync();
        }

        public async Task Add(Board board)
        {
            _dbContext.Boards.Add(board);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Board board)
        {
            _dbContext.Boards.Update(board);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var boardToRemove = await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == id);
            if (boardToRemove is null)
                throw new ArgumentException("Board with given Id doesn't exist");

            _dbContext.Boards.Remove(boardToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetCount()
        {
           return await _dbContext.Boards.CountAsync();
        }

    }
}
