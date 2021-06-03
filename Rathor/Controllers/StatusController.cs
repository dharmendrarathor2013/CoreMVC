using Microsoft.AspNetCore.Mvc;
using Rathor.Data;
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rathor.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StatusController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Status> objList = _db.Status;

            return View(objList);
        }
        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        // Post - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Status obj)
        {
            if (ModelState.IsValid)
            {
                _db.Status.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Status.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Status obj)
        {
            if (ModelState.IsValid)
            {
                _db.Status.Update(obj);
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
            var obj = _db.Status.Find(id);
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
            var obj = _db.Status.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Status.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
