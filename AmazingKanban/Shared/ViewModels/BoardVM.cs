using AmazingKanban.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.ViewModels
{
    public class BoardVM
    {
        public Board Board { get; set; } = new Board();
        public List<BoardUserAccess> UserAccesses { get; set; } = new List<BoardUserAccess>();
    }
}
