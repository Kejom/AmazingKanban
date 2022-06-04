using AmazingKanban.Server.Data;
using AmazingKanban.Shared;
using AmazingKanban.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazingKanban.Server.Repositories
{
    public class BoardAccessRepository : IBoardAccessRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BoardAccessRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BoardAccess<ApplicationUser>>> GetAll()
        {
            return await _dbContext.BoardAccesses.Include(a => a.User).ToListAsync();
        }
        public async Task<BoardAccess<ApplicationUser>> GetById(int id)
        {
            var result = await _dbContext.BoardAccesses.FirstOrDefaultAsync(a => a.Id == id);

            if(result is null)
                throw new ArgumentException("Board Access with given Id doesnt exist");

            return result;
        }
        public async Task<List<BoardAccess<ApplicationUser>>> GetByBoardId(int boardId)
        {
            return await _dbContext.BoardAccesses.Where(a => a.BoardId == boardId).Include(a => a.User).ToListAsync();
        }

        public async Task<List<BoardAccess<ApplicationUser>>> GetByUserId(string userId)
        {
            return await _dbContext.BoardAccesses.Where(a => a.UserId == userId).Include(a => a.User).ToListAsync();
        }

        public async Task<BoardAccess<ApplicationUser>> GetByUserAndBoardId(string userId, int boardId)
        {
            var access = await _dbContext.BoardAccesses.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId && a.BoardId == boardId);

            if (access is null)
            {
                var user = await _dbContext.Users.FirstAsync(u => u.Id == userId);
                return new BoardAccess<ApplicationUser> { BoardId = boardId, UserId = userId, User = user, Role = BoardRoles.NoAccess };
            }


            return access;
        }

        public async Task Add(BoardAccess<ApplicationUser> boardAccess)
        {
            await _dbContext.BoardAccesses.AddAsync(boardAccess);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(BoardAccess<ApplicationUser> boardAccess)
        {
            _dbContext.BoardAccesses.Update(boardAccess);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int accessId)
        {
            var accessToRemove = await _dbContext.BoardAccesses.FirstOrDefaultAsync(a => a.Id == accessId);

            if (accessToRemove is null)
                throw new ArgumentException("Board Access with given Id doesnt exist");

            _dbContext.BoardAccesses.Remove(accessToRemove);
            _dbContext.SaveChanges();
        }
    }
}
