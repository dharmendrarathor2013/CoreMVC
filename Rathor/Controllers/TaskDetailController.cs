using Microsoft.AspNetCore.Mvc;
using Rathor.Data;
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Rathor.Controllers
{
    public class TaskDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment hostingEnvironment;
        private string FileName;

        public TaskDetailController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Test");

            TaskUserList taskuserlist = new TaskUserList();
            taskuserlist.TaskDetail = _db.TaskDetail.ToList();
           
            taskuserlist.Sprint = _db.Sprint.ToList();
            taskuserlist.User = _db.User.ToList();
            taskuserlist.TaskType = _db.TaskType.ToList();
            taskuserlist.Status = _db.Status.ToList();

            return View(taskuserlist);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public void SaveFiles(TaskUserViewModel filelist)
        //{
        //    string uniqueFileName = null;
        //    foreach (IFormFile file in filelist.IFormFiles)
        //    {
        //        //foreach (IFormFile file in filelist.IFormFiles)
        //        //{
        //        Document document = new Document();
        //        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //        FileName = file.FileName;

        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        file.CopyTo(new FileStream(filePath, FileMode.Create));

        //        document.FileName = uniqueFileName;
        //        document.FilePath = filePath;
        //        document.CreateDate = System.DateTime.Now;
        //        _db.Document.Add(document);
        //    }
        //    _db.SaveChanges();

        //}
        public IActionResult AddMoreFile()
        {
            return View();
        }
        public void SaveFiles(TaskUserViewModel filestaskdetail)
        {
            string uniqueFileName = null;
            foreach (IFormFile file in filestaskdetail.Files)
            {
                TaskDocument filedata = new TaskDocument();
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                FileName = file.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                filedata.TaskID = filestaskdetail.TaskDetail.TaskID;
                filedata.FileName = uniqueFileName;
                filedata.FilePath = filePath;
                filedata.CreateDate = System.DateTime.Now;
                filedata.CreatedBy = filestaskdetail.TaskDetail.AssignyId;
                _db.TaskDocuments.Add(filedata);
            }
            _db.SaveChanges();

        }
        //public async Task<IActionResult> Download(int? id)
        //{
        //    var data = _db.TaskDetail.Find(id);
        //    var filename = data.FilePath.ToString();
        //    if (filename == null)
        //        return Content("filename not present");

        //    var path = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files", filename);

        //    //Path.Combine(
        //    //               Directory.GetCurrentDirectory(),
        //    //               "wwwroot", filename);

        //    //var memory = new MemoryStream();
        //    //using (var stream = new FileStream(path, FileMode.Open))
        //    //{
        //    //    await stream.CopyToAsync(memory);
        //    //}
        //    //memory.Position = 0;
        //    return Redirect(path);
        //    //return File(memory, path, Path.GetFileName(path));
        //}
        //public FileResult Download(int? id)
        //{ 

        //    TaskUserViewModel tasklist = new();
        //    tasklist.TaskDetail = _db.TaskDetail.Find(id);
        //    var FileName = tasklist.TaskDetail.FilePath;
        //    // string uniqueFileName = null;
        //    var filePath = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files");
        //    //var fileExists = System.IO.File.Exists(filePath);
        //    //string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files");
        //    //string filePath = Path.Combine(uploadsFolder, FileName);
        //    //return File(filePath, "image/PNG", "Rathor.png");
        //    return File((filePath+"\\"+ FileName), "Image/PNG", FileName);
        //}

        // GET - CREATE
        public IActionResult Create()
        {
            TaskUserViewModel tasklist = new();
            ViewBag.Sprint = new SelectList(_db.Sprint.ToList(), "SprintId", "Name");
            ViewBag.TaskType = new SelectList(_db.TaskType.ToList(), "TypeID", "Name");
            ViewBag.TaskDetail = new SelectList(_db.TaskDetail.ToList(), "TaskId", "Title");
            ViewBag.User = new SelectList(_db.User.ToList(), "AssignyId", "Name");
            ViewBag.Status = new SelectList(_db.Status.ToList(), "StatusId", "Name");

            
            return View();
        }

        // Post - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskUserViewModel data)
        {
            data.TaskDetail.CreateDate = System.DateTime.Now;
            data.TaskDetail.LastModificationDate = System.DateTime.Now;
            data.TaskDetail.CreatedBy = data.TaskDetail.AssignyId;
            data.TaskDetail.ModifiedBy = 1;
            data.TaskDetail.IsActive = true;
            data.TaskDetail.IsDeleted = false;

            _db.TaskDetail.Add(data.TaskDetail);
            _db.SaveChanges();

            this.SaveFiles(data);
            return RedirectToAction("Index");
        }

        // Post - SaveFile
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public TaskDocument SaveFile(IFormFile File, TaskDetail TaskDetail)
        //{
        //    string uniqueFileName = null;
        //    TaskDocument filedata = new TaskDocument();
        //    //if (File != null && File.Count > 0)
        //    //{
        //        //foreach (IFormFile file in File)
        //        //{
        //        //    _db.SaveChanges();
        //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploaded_files");
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + File.FileName;
        //            FileName = File.FileName;

        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //             File.CopyTo(new FileStream(filePath, FileMode.Create));
        //             filedata.TaskID = TaskDetail.TaskID;
        //             filedata.FileName = uniqueFileName;
        //             filedata.FilePath = filePath;
        //             filedata.CreateDate = TaskDetail.CreateDate;
        //             filedata.CreatedBy = TaskDetail.AssignyId;
        //    return filedata;
        //    //}
        //    //}

        //}
        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TaskUserViewModel tasklist = new();
            tasklist.TaskDetail = _db.TaskDetail.Find(id);
            tasklist.UserList = _db.User.ToList();
            tasklist.StatusList = _db.Status.ToList();
            tasklist.TaskTypeList = _db.TaskType.ToList();
            tasklist.SprintList = _db.Sprint.ToList();

            //if (tasklist.TaskDetail == null && tasklist.UserList == null && tasklist.TaskTypeList == null && tasklist.StatusList == null && tasklist.SprintList == null)
            //{
            //    return NotFound();
            //}

            return View(tasklist);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskUserViewModel user)
        {
            if (user == null)
            {
                return NotFound();
            }
           
            user.TaskDetail.LastModificationDate = DateTime.Now;
            user.TaskDetail.ModifiedBy = user.TaskDetail.AssignyId;
            var obj = user.TaskDetail;
            _db.TaskDetail.Update(obj);
            _db.SaveChanges();
                return RedirectToAction("Index");  
        }

        // Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TaskUserViewModel tasklist = new();
            tasklist.TaskDetail = _db.TaskDetail.Find(id);
            tasklist.UserList = _db.User.ToList();
            tasklist.StatusList = _db.Status.ToList();
            tasklist.TaskTypeList = _db.TaskType.ToList();
            tasklist.SprintList = _db.Sprint.ToList();

            if (tasklist.TaskDetail == null && tasklist.UserList == null && tasklist.TaskTypeList == null && tasklist.StatusList == null && tasklist.SprintList == null)
            {
                return NotFound();
            }

            return View(tasklist);
        }

        // Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, string s)
        {
            var obj = _db.TaskDetail.Find(id);
            if(obj==null)
            {
                return NotFound();
            }
                _db.TaskDetail.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
        public IActionResult Menuebar()
        {
            List<MenueDetails> menuelist = new List<MenueDetails>();
            menuelist.Add(new MenueDetails
            {
                Title = "Task Type",
                Discription = "A task is an activity that needs to be accomplished within a defined period of time or by a deadline to work towards work-related goals.",
                RecordsCount = _db.TaskType.Count(),
                URL = "/TaskType/Index"
            });
            menuelist.Add(new MenueDetails
            {
                Title = "Status",
                Discription = "Project status reports are taken repeatedly, as a means to maintain your schedule and keep everyone on the same page.",
                RecordsCount = _db.Status.Count(),
                URL = "/Status/Index"
            });
           
            menuelist.Add(new MenueDetails
            {
                Title = "Sprint",
                Discription = "The sprint is the time-boxed period that we allocate to content delivery.",
                RecordsCount = _db.Sprint.Count(),
                URL = "/Sprint/Index"
            });
            menuelist.Add(new MenueDetails
            {
                Title = "Category",
                Discription = "You will see categories like scope and significance, type of the project, control, and implementations.",
                RecordsCount = _db.Category.Count(),
                URL = "/Category/Index"
            });
            menuelist.Add(new MenueDetails
            {
                Title = "Application Type",
                Discription = "It will go through the type of application that we are using in our project.",
                RecordsCount = _db.ApplicationType.Count(),
                URL = "/ApplicationType/Index"
            });
            return View(menuelist);
        }
    }
}
