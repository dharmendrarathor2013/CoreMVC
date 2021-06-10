using Rathor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class Project
    {
        public Project()
        {
            //User
            if (this.AssignyId != 0)
            {
                this.User = Db.User.Where(x => x.AssignyId == this.AssignyId).FirstOrDefault();
            }
            Sprints = new HashSet<Sprint>();
        }
        [Key]
        [Required]
        public int projectId { set; get; }
        [Required]
        public string Title { set; get; }
        [Required]
        public string Description { set; get; }
        public int AssignyId { set; get; }
        public int UpdateBy { set; get; }
        public Boolean DeleteStatus { set; get; }
        public Boolean Complete { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { set; get; }
        [Required]
        public DateTime LastModification { set; get; }
        [Required]
        public ApplicationDbContext Db { get; }

        //Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Sprint> Sprints { get; set; }
    }
}
