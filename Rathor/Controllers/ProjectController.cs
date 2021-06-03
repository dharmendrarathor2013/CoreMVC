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
            ViewProject projectList = new ViewProject();

            projectList.User = _db.User.ToList();
            projectList._projectInfo = _db.Project.ToList();
            return View(projectList);
        }
        // Get-Create
        public IActionResult CreateProject()
        {
            ProjectInfo projectList = new ProjectInfo();

            projectList.userlist = _db.User.ToList();

            return View(projectList);

        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(ProjectInfo data)
        {
            var obj = data._projectInfo;

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
            ProjectInfo projectlist = new();
            projectlist._projectInfo  = _db.Project.Find(id);
            projectlist.userlist = _db.User.ToList();

            if (projectlist._projectInfo == null && projectlist.userlist == null)
            {
                return NotFound();
            }

            return View(projectlist);
        }
        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(ProjectInfo data)
        {
            data._projectInfo.LastModification = DateTime.Now;
            data._projectInfo.UpdateBy = data._projectInfo.AssignyId;
            var obj = data._projectInfo;
            _db.Project.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Get - Delete
        public IActionResult DeleteProject(int? id)
        {
            ProjectInfo projectlist = new();
            projectlist._projectInfo = _db.Project.Find(id);
            projectlist.userlist = _db.User.ToList();

            if (projectlist._projectInfo == null && projectlist.userlist == null)
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
