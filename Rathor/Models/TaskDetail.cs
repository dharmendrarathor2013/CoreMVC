using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using Xunit.Sdk;
using Rathor.Data;

namespace Rathor.Models

{
    [Table("TaskDetail")]
    public class TaskDetail
    {
        public TaskDetail()
        {
           //TaskTye
            if (this.TaskID != 0)
            {
                this.TaskType = Db.TaskType.Where(x => x.TypeID == this.TypeID).FirstOrDefault();
            }

            //User
            if (this.AssignyId != 0)
            {
                this.User = Db.User.Where(x => x.AssignyId == this.AssignyId).FirstOrDefault();
            }

            //Status
            if (this.StatusId != 0)
            {
                this.Status = Db.Status.Where(x => x.StatusId == this.StatusId).FirstOrDefault();
            }

            //Sprint
            if (this.SprintId != 0)
            {
                this.Sprint = Db.Sprint.Where(x => x.SprintId == this.SprintId).FirstOrDefault();
            }
            TaskDocuments = new HashSet<TaskDocument>();
        }
        [Key]
        [Column("TaskID")]
        public int TaskID { get; set; }
        [Required]
        [Column("Title")]
        [MaxLength(15)]
        public string Title { get; set; }  
        [Required]
        [Column("Discription")]
        public string Discription { get; set; }
        [Display(Name = "Task Type")]
        public int TypeID { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Display(Name = "Assignee")]
        public int AssignyId { get; set; }
        [Display(Name = "Sprint")]
        public int SprintId { get; set; }
        [Required( ErrorMessage = "Hours field is required")]
        [Range(1,50, ErrorMessage = "Number must be in range 1-50")]
        public int Hours { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "Start Date")]
        [MustBeGreaterThan("EndDate", "Start date must be greater than End date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "End Date")]
        
        //[DateGreaterThanAttribute(otherPropertyName = "StartDate", ErrorMessage = "End date must be greater than start date")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastModificationDate { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public ApplicationDbContext Db { get; }

        // Navigation Properties
        public virtual TaskType TaskType { get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual ICollection<TaskDocument> TaskDocuments { get; set; }
    }
}

    

