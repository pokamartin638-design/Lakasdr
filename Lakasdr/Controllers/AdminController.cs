using Lakasdr.Data;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lakasdr.Controllers
{
    public class AdminController : Controller
    {
        private readonly WorkDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AdminController(WorkDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        private const string FixUsername = "admin";
        private const string FixPassword = "1234";

        [HttpGet]
        public IActionResult Admin()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Admin(string username, string password)
        {
            if (username == FixUsername && password == FixPassword)
            {
                TempData["LoginSuccess"] = true;
                return RedirectToAction("AdminFelulet");
            }

            ViewBag.LoginStatus = "error";
            return View();
        }
        public IActionResult AdminFelulet()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult WorkersUpdate(Workers munkas)
        {
            _db.Add(munkas);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
