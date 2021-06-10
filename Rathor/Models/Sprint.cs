using Rathor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class Sprint
    {
        public Sprint()
        {
            //Project
            //if (this.projectId != 0)
            //{
            //    this.Project = Db.Project.Where(x => x.projectId == this.projectId).FirstOrDefault();
            //}
        }
        [Key]
        public int SprintId { get; set; }
        [Required]
        [Display(Name = "Sprint")]
        public string  Name { get; set; }
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "End Date")]
         public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastModificationDate { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int? projectId { set; get; }
        public ApplicationDbContext Db { get; }

        public virtual ICollection<TaskDetail> TaskDetail { get; set; }
       public virtual Project Project { get; set; }
    }
}
