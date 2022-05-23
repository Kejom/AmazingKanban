using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingKanban.Shared
{
    public enum KanbanTaskStates
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "To Do")]
        ToDo,
        [Display(Name ="In Progress")]
        InProgress,
        [Display(Name = "Testing")]
        Testing,
        [Display(Name = "Done")]
        Done

    }
}
