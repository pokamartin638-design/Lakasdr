using Lakasdr.Data;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lakasdr.Controllers
{
 
    
    public class HomeController : Controller
    {
        private readonly WorkDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HomeController(WorkDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Bemutatkozas()
        {
            return View();
        }

//--------------------------------------------------------------------------------------------------------
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
//--------------------------------------------------------------------------------------------------------
        public IActionResult Update()
        {
            return View();
        }
//--------------------------------------------------------------------------------------------------------
        public IActionResult ImageUpdate()
        {
            return View();
        }
//--------------------------------------------------------------------------------------------------------
        public IActionResult WorkersUpdate()
        {
            return View();
        }

//--------------------------------------------------------------------------------------------------------
       public IActionResult Calculator()
        {
            return View();
        }
        public IActionResult Kiszamol()
        {


            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_env.WebRootPath, "images");
                string filePath = Path.Combine(uploads, file.FileName);

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _db.Images.Add(new Image
                {
                    Nev = file.Name,
                    UploadDate = DateTime.Now
                });
                _db.SaveChanges();
            }
            return RedirectToAction("Gallery");
        }
        public IActionResult Gallery()
        {
            var images = _db.Images.ToList();
            return View(images);
        }

//--------------------------------------------------------------------------------------------------------
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
