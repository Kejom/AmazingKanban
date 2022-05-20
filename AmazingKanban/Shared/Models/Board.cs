using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser? Owner { get; set; }
        [Required(ErrorMessage ="Board Name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
