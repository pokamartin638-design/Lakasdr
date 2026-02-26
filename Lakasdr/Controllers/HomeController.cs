using Lakasdr.Data;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace Lakasdr.Controllers
{
 
    
    public class HomeController : Controller
    {
        private readonly WorkDbContext _context;
        private readonly IWebHostEnvironment _env;
        public HomeController(WorkDbContext context, IWebHostEnvironment env)
        { 
            _context = context;
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
        public IActionResult Index1()
        {
            var images = _context.Images.ToList();
            return View(images);
        }

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

            _context.Images.Add(image);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var img = _context.Images.FirstOrDefault(x => x.Id == id);
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

            _context.Images.Remove(img);
            _context.SaveChanges();

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
