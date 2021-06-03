using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class User
    {
        [Key]
        public int AssignyId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Dob { get; set; }
        public virtual ICollection<TaskDetail> TaskDetail { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

}
