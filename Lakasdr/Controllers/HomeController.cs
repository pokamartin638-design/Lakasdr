using System.Diagnostics;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lakasdr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Bemutatkozas()
        {
            return View();
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


     
        public IActionResult Update()
        {
            return View();
        }

        public IActionResult ImageUpdate()
        {
            return View();
        }

        public IActionResult WorkersUpdate()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
