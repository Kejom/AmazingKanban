using Microsoft.AspNetCore.Identity;

namespace AmazingKanban.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;

        public UserLite ConvertToUserLite()
        {
            return new UserLite
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Id = this.Id
            };
        }
    }


}