using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Models
{
    public class SprintDetails
    {
       public string Project_Title { get; set; }
        public List<Sprint> SprintList { get; set; }
        public List<TaskDetail> TaskDetailList { get; set; }
        public List<User> UserList { get; set; }
        public List<Status> StatusList { get; set; }
    }
}
