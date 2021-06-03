using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Rathor.Models;

namespace Rathor.Controllers
{
    public class UploadFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult SaveFiles()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFiles(TaskUserViewModel taskuserdetail)
        {
            return View();
        }
    }
}
