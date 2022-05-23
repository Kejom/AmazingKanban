using AmazingKanban.Shared.Models;

namespace AmazingKanban.Server.Factories
{
    public class UserLiteFactory : IUserLiteFactory
    {

        public UserLite Convert(ApplicationUser user)
        {
            return new UserLite
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
        public List<UserLite> Convert(IEnumerable<ApplicationUser> users)
        {
            return users.Select(u => Convert(u)).ToList();
        }
    }
}
