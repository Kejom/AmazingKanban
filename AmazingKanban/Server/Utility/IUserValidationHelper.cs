using AmazingKanban.Shared;
using System.Security.Claims;

namespace AmazingKanban.Server.Utility
{
    public interface IUserValidationHelper
    {
        Task<bool> ValidateBoardAccess(int boardId, ClaimsPrincipal User, BoardRoles accessLevel);
    }
}