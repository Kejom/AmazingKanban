using AmazingKanban.Server.Repositories;
using AmazingKanban.Shared;
using System.Security.Claims;

namespace AmazingKanban.Server.Utility
{
    public class UserValidationHelper : IUserValidationHelper
    {
        private readonly IBoardAccessRepository _boardAccessRepository;

        public UserValidationHelper(IBoardAccessRepository boardAccessRepository)
        {
            _boardAccessRepository = boardAccessRepository;
        }

        public async Task<bool> ValidateBoardAccess(int boardId, ClaimsPrincipal User, BoardRoles accessLevel)
        {
            var isAdmin = User.IsInRole(UserRoles.Admin.ToString());
            if (isAdmin)
                return true;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var boardAccess = await _boardAccessRepository.GetByUserAndBoardId(userId, boardId);
            return boardAccess.Role >= accessLevel;
        }
    }
}
