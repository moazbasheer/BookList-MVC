using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsersController : Controller
    {
        public readonly ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Logout()
        {
            if(HttpContext.Session.Get("email") != null)
            {
                HttpContext.Session.Remove("email");
            }
            return RedirectToAction("Login", "Users", new { area = "" });
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.Get("email") != null)
            {
                return RedirectToAction("Index", "Books", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(User User)
        {
            var user = _db.Users.FirstOrDefault(e => e.Email == User.Email && e.Password == User.Password);
            if(user == null)
            {
                // not a user.
                return View();
            }
            HttpContext.Session.SetString("email", user.Email);
            
            // a user
            return RedirectToAction("Index", "Books", new { area = "" });
        }

        public IActionResult Register()
        {
            //This would be repeated every where
            //violates DRY principle 
            if (HttpContext.Session.Get("email") != null)
            {
                return RedirectToAction("Index", "Books", new { area = "" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(User User)
        {
            
            _db.Users.Add(User);
            _db.SaveChanges();
            return View();
        }
    }
}
