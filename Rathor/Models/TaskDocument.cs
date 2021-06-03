using Rathor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    [Table("TaskDocument")]
    public class TaskDocument
    {
        public TaskDocument()
        {
            if (this.Id != 0)
            {
                this.TaskDetail = Db.TaskDetail.Where(x => x.TaskID == this.TaskID).FirstOrDefault();
            }
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskID { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required(ErrorMessage = "File must be there")]
        [Display(Name = "Please upload file ")]
        public string FilePath { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public ApplicationDbContext Db { get; }

        // Refference 
        public virtual TaskDetail TaskDetail { get; set; }
    }
}
