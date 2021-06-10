using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rathor.Data;
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProjectController(ApplicationDbContext DB)
        {
            _db = DB;

        }
        public IActionResult Index()
        {
            //if (HttpContext.Session.GetString("id") != null)
            //{
            //    var id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            //    User data = _db.User.Where(x => x.AssignyId == id).FirstOrDefault();
            //    ViewBag.Message = data.Name;
            //    ViewProject projectList = new ViewProject();

            //    projectList.User = _db.User.ToList();
            //    projectList._projectInfo = _db.Project.ToList();
            //    return View(projectList);

            //}
            //else
            //{
            //    return View("Login", "User");
            //}   
            TaskUserList projectList = new TaskUserList();

            projectList.User = _db.User.ToList();
            projectList.Project = _db.Project.ToList();
            return View(projectList);
        }
        //Get Cards
        public IActionResult Cards(int? id)
        {
            TaskUserList projectList = new TaskUserList();

            projectList.User = _db.User.ToList();
            projectList.Project = _db.Project.ToList();
            return View(projectList);
        }
        
        // Get Sprint deatils 
        public IActionResult Sprint(int? id)
        {
            var data = _db.Project.Where(x => x.projectId == id).FirstOrDefault();

            if (data != null)
            {
                ViewBag.ProjectTitle = data.Title;
            }

            SprintDetails sprintlist = new SprintDetails();

            sprintlist.SprintList = _db.Sprint.Where(t => t.projectId == id).ToList();
            sprintlist.TaskDetailList = _db.TaskDetail.ToList();
            sprintlist.UserList = _db.User.ToList();
            
            return View(sprintlist);   
        }

        //Get Task List
         
        public IActionResult TaskList()
        {
            SprintDetails sprintlist = new SprintDetails();
            sprintlist.TaskDetailList = _db.TaskDetail.ToList();
            sprintlist.UserList = _db.User.ToList();
            sprintlist.StatusList = _db.Status.ToList();
            return View(sprintlist);
        }
        // Get-Create
        public IActionResult CreateProject()
        {
            TaskUserViewModel projectList = new TaskUserViewModel();

            projectList.UserList = _db.User.ToList();

            return View(projectList);

        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(TaskUserViewModel data)
        {
            var obj = data.Project;

            obj.CreateDate = DateTime.Now;
            obj.LastModification = DateTime.Now;
            
            _db.Project.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get - Edit
        public IActionResult EditProject(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskUserViewModel projectlist = new();
            projectlist.Project  = _db.Project.Find(id);
            projectlist.UserList = _db.User.ToList();

            if (projectlist.Project == null && projectlist.UserList == null)
            {
                return NotFound();
            }

            return View(projectlist);
        }
        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(TaskUserViewModel data)
        {
            data.Project.LastModification = DateTime.Now;
            data.Project.UpdateBy = data.Project.AssignyId;
            var obj = data.Project;
            _db.Project.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Get - Delete
        public IActionResult DeleteProject(int? id)
        {
            TaskUserViewModel projectlist = new();
            projectlist.Project = _db.Project.Find(id);
            projectlist.UserList = _db.User.ToList();

            if (projectlist.Project == null && projectlist.UserList == null)
            {
                return NotFound();
            }
            return View(projectlist);
        }
        // Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int? id, string s)
        {
            var obj = _db.Project.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Project.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
