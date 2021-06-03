using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskDetail> TaskDetail { get; set; }
    }
}
