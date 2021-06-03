using Microsoft.AspNetCore.Mvc;
using Rathor.Data;
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Rathor.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Test");
            IEnumerable<User> list = _db.User;
            return View(list);
        }
        // GET - CREATE
        public IActionResult SignUp()
        {
            return View();
        }

        // Post - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(User obj)
        {
            
                var mail = obj.Email;
                var data = _db.User.Where(x => x.Email == mail).FirstOrDefault();

                if (data != null)
                {
                    ViewBag.mailexist = "Mail Id is already exist";
                    return RedirectToAction("SignUp", "User");
                }

                else
                {
                    _db.User.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User obj)
        {
           
            if (obj.Email != null)
            { 
            HttpContext.Session.SetString("Test", obj.Email);
            }

            var data = _db.User.Where(x => x.Email == obj.Email && x.Password == obj.Password).FirstOrDefault();
            if (data != null)
            {
                return RedirectToAction("Index", "TaskDetail");
            }

            else
            {
                ViewBag.error = "invalid email or password";
            }

            return View(obj);
        }

        // GET - ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        // Post - ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(User obj)
        {
            var mail = obj.Email;
            var oldpassword = obj.OldPassword;

            var maildata = _db.User.Where(x => x.Email == mail).FirstOrDefault();
            var oldpassworddata = _db.User.Where(x => x.Password == oldpassword).FirstOrDefault();
            if(obj.OldPassword == obj.Password)
            {
                TempData["samepassword"] = "Old and New Password must be different";
                return RedirectToAction("ChangePassword");
            }
            else if (maildata == null)
            {
                TempData["mail"] = "User doesn't exist";
                return RedirectToAction("ChangePassword");
            }

            else if (oldpassworddata == null)
                {
                    TempData["oldpassword"] = "Old Password is invalid";
                    return RedirectToAction("ChangePassword");
                }

            else
            {
                User user = _db.User.Where(x => x.Email == obj.Email).FirstOrDefault();                
                user.Password = obj.Password;
                _db.SaveChanges();
                TempData["passwordchanged"] = "Password changed successfully";
                return RedirectToAction("ChangePassword");
            }
        }

        // GET - ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Post - ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(User obj)
        {
            if (obj.Password != obj.ConfirmPassword)
            {
                TempData["confirmpassword"] = "New Password and Confirm New Password should be same";
                return RedirectToAction("ForgotPassword");
            }
            
            var maildata = _db.User.Where(x => x.Email == obj.Email).FirstOrDefault();
          
            if (maildata.Email != obj.Email || maildata.Name != obj.Name)
            {
                TempData["mailname"] = "User doesn't exist, Please enter correct Mail Id or User Name";
                return RedirectToAction("ForgotPassword");
            }

            else
            {
                User user = _db.User.Where(x => x.Email == obj.Email).FirstOrDefault();
                user.Password = obj.Password;
                _db.SaveChanges();
                if(user.Email != null)
                {
                TempData["passwordreset"] = "New Password has been generated successfully";

                    return RedirectToAction("ForgotPassword");
                }
                return View(obj);
            }
        }
    }
}



