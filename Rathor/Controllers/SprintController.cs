using Microsoft.AspNetCore.Mvc;
using Rathor.Data;
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Controllers
{
    public class SprintController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SprintController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            TaskUserList sprintlist = new TaskUserList();
            sprintlist.Project = _db.Project.ToList();
            sprintlist.Sprint = _db.Sprint.ToList();
           // IEnumerable<Sprint> objList = _db.Sprint;

            return View(sprintlist);
        }
        // GET - CREATE
        public IActionResult Create()
        {
            TaskUserViewModel sprintdata = new TaskUserViewModel();

            sprintdata.ProjectList = _db.Project.ToList();

            return View(sprintdata);
        }

        // Post - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskUserViewModel data)
        {
            var obj = data.Sprint;
            obj.CreateDate = DateTime.Now;
            obj.LastModificationDate = DateTime.Now;
            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.IsDeleted = false;
            
            _db.Sprint.Add(obj);
            _db.SaveChanges();
           return RedirectToAction("Index");
           
            
        }

        // Get - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Sprint.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sprint obj)
        {

            if (ModelState.IsValid)
            {
                obj.LastModificationDate = DateTime.Now;
                _db.Sprint.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Sprint.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, string s)
        {
            var obj = _db.Sprint.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Sprint.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
