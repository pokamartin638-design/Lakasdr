using Lakasdr.Data;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using System.IO;
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
        

        // UPLOAD FORM
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // UPLOAD POST
        [HttpPost]
        public IActionResult Upload(string nev, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return View();

            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLower();

            if (!allowed.Contains(ext))
                return View();

            var fileName = Guid.NewGuid().ToString() + ext;
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var image = new Image
            {
                Nev = nev,
                FilePath = "/uploads/" + fileName,
                UploadDate = DateTime.Now
            };

            _db.Images.Add(image);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var img = _db.Images.FirstOrDefault(x => x.Id == id);
            if (img == null) return NotFound();

            if (!string.IsNullOrEmpty(img.FilePath))
            {
                var physicalPath = Path.Combine(
                    _env.WebRootPath,
                    img.FilePath.TrimStart('/')
                );

                if (System.IO.File.Exists(physicalPath))
                    System.IO.File.Delete(physicalPath);
            }

            _db.Images.Remove(img);
            _db.SaveChanges();

            return RedirectToAction("Index");
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
