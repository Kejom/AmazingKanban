using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Factories
{
    public interface IUserLiteFactory
    {
        UserLite Convert(ApplicationUser user);
        List<UserLite> Convert(IEnumerable<ApplicationUser> users);
    }
}