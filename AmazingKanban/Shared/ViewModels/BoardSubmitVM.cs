using AmazingKanban.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.ViewModels
{
    public class BoardSubmitVM
    {
        public Board Board { get; set; } = new Board();
        public List<BoardAccess<UserLite>> UserAccesses { get; set; } = new List<BoardAccess<UserLite>>();
    }
}
