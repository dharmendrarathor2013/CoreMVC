using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class TaskUserList
    {
        public List<TaskDetail> TaskDetail { get; set; }
        public List<TaskDocument> TaskDocuments { get; set; }
        public List<Project> Project { get; set; }
        public List<Sprint> Sprint { get; set; }
        public List<User> User { get; set; }
        public List<TaskType> TaskType { get; set; }
        public List<Status> Status { get; set; }
        public List<Category> Category { get; set; }
        public List<ApplicationType> ApplicationType { get; set; }


    }
}
