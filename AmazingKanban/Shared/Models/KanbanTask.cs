using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.Models
{
    public class KanbanTask<T>
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        [Required]
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } = null;
        public DateTime? ClosedOn { get; set; } = null;
        public bool ShowOnBoard { get; set; }
        [Range(1, 4)]
        public int Priority { get; set; } = 4;
        public KanbanTaskStates State { get; set; }
        public string? CreatedById { get; set; } 
        [ForeignKey("CreatedById")]
        public T? CreatedBy { get; set; }
        public string? AssignedToId { get; set; } 
        [ForeignKey("AssignedToId")]
        public T? AssignedTo { get; set; }
        public string? ValidatorId { get; set; }
        [ForeignKey("ValidatorId")]
        public T? Validator { get; set; }

    }
}
