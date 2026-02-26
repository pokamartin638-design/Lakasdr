//using Lakasdr.Data;
//using Lakasdr.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace Lakasdr.Controllers
//{
//    public class ImagesController : Controller
//    {
//        private readonly WorkDbContext _context;
//        private readonly IWebHostEnvironment _env;

//        public ImagesController(WorkDbContext context, IWebHostEnvironment env)
//        {
//            _context = context;
//            _env = env;
//        }

//        // LISTA
//        public IActionResult Index()
//        {
//            var images = _context.Images.ToList();
//            return View(images);
//        }

//        // UPLOAD FORM
//        [HttpGet]
//        public IActionResult Upload()
//        {
//            return View();
//        }

//        // UPLOAD POST
//        [HttpPost]
//        public IActionResult Upload(string nev, IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return View();

//            var allowed = new[] { ".jpg", ".jpeg", ".png" };
//            var ext = Path.GetExtension(file.FileName).ToLower();

//            if (!allowed.Contains(ext))
//                return View();

//            var fileName = Guid.NewGuid().ToString() + ext;
//            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            var fullPath = Path.Combine(uploadPath, fileName);

//            using (var stream = new FileStream(fullPath, FileMode.Create))
//            {
//                file.CopyTo(stream);
//            }

//            var image = new Image
//            {
//                Nev = nev,
//                FilePath = "/uploads/" + fileName,
//                UploadDate = DateTime.Now
//            };

//            _context.Images.Add(image);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // DELETE
//        public IActionResult Delete(int id)
//        {
//            var img = _context.Images.FirstOrDefault(x => x.Id == id);
//            if (img == null) return NotFound();

//            if (!string.IsNullOrEmpty(img.FilePath))
//            {
//                var physicalPath = Path.Combine(
//                    _env.WebRootPath,
//                    img.FilePath.TrimStart('/')
//                );

//                if (System.IO.File.Exists(physicalPath))
//                    System.IO.File.Delete(physicalPath);
//            }

//            _context.Images.Remove(img);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }
//    }
//}