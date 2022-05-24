using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.Models
{
    public class TaskComment<T>
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int TaskId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } = null;
        public string CreatedById { get; set; } = string.Empty;
        [ForeignKey("CreatedById")]
        public T? CreatedBy { get; set; }
        [Required]
        [MinLength(5)]
        public string CommentText { get; set; } = string.Empty;
        public bool ShowOnBoard { get; set; }
    }
}
