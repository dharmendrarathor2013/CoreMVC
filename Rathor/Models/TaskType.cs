using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class TaskType
    {
        [Key]
        public int TypeID { get; set; }
        [Display(Name= "Task Type")]
        public string Name { get; set; } 
        public virtual ICollection<TaskDetail> TaskDetail { get; set; }
    }
}
