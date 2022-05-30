using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.Models
{
    public class BoardAccess<T>
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Board? Board { get; set; }
        public string UserId { get; set; } = String.Empty;
        [ForeignKey("UserId")]
        public T? User { get; set; }
        public BoardRoles Role { get; set; }
    }
}
