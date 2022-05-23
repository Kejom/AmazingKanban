using AmazingKanban.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.ViewModels
{
    public class BoardUserVM
    {
        public UserLite User { get; set; } = new UserLite();
        public BoardRoles? BoardRole { get; set; }
    }
}
