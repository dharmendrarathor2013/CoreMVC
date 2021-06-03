using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    
    public class TaskUserViewModel
      {
        public TaskDetail TaskDetail { get; set; }
        [Required]
        public List<IFormFile> Files { get; set; }
        public List<IFormFile> newIformfiles { get; set; }
        public List<TaskType> TaskTypeList { get; set; }
        public List<Status> StatusList { get; set; }
        public List<User> UserList { get; set; }
        public List<Sprint> SprintList { get; set; }
        public Sprint Sprint { get; set; }
        public List<Project> ProjectList { get; set; }
    }
}
