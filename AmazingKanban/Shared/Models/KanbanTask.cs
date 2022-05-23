using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared.Models
{
    public class KanbanTask
    {
        [Key]
        public int Id { get; set; }
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
        public string CreatedById { get; set; } = String.Empty;
        [ForeignKey("CreatedById")]
        public ApplicationUser? CreatedBy { get; set; }
        public string AssignedToId { get; set; } = String.Empty;
        [ForeignKey("AssignedToId")]
        public ApplicationUser? AssignedTo { get; set; }
        public string ValidatorId { get; set; } = String.Empty;
        [ForeignKey("ValidatorId")]
        public ApplicationUser? Validator { get; set; }

    }
}
