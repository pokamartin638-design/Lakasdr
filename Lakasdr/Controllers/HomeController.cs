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
        [HttpPost]
        public IActionResult Kiszamol(int kategoria, int terulet)
        {
            int ossz = 0;


            switch(kategoria)
            {
                case 1:
                    ossz = ossz + terulet * 3000;
                    break;

                case 2:
                    ossz = ossz + terulet * 6000;
                    break;

                case 3:
                    ossz = ossz + terulet * 4500;
                    break;

                case 4:
                    ossz = ossz + terulet * 5000;
                    break;

                case 5:
                    ossz = ossz + terulet * 6000;
                    break;

                

                case 7:
                    ossz = ossz + terulet * 5000;
                    break;

                case 8:
                    ossz = ossz + terulet * 4000; //!oradij!
                    break;

                case 9:
                    ossz = ossz + terulet * 5000; //!oradij!
                    break;

                case 10:
                    ossz = ossz + terulet * 5000; //!oradij!
                    break;

                case 11:
                    ossz = ossz + terulet * 4000; //!oradij!
                    break;
            }
                


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
