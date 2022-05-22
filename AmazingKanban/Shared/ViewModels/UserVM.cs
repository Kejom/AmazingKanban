using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.ViewModels
{
    public class UserVM
    {
        public string? Id { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; } = String.Empty;
        public BoardRoles? BoardRole { get; set; }
        public List<UserRoles> Roles { get; set; } = new List<UserRoles>();
    }
}
