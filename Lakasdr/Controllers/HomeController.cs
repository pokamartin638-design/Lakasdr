using Lakasdr.Data;
using Lakasdr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using System.IO;
using System.Text;
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
        [HttpGet]
        public IActionResult Munkatars(int? id)
        {
            ViewBag.Workers = _db.Workers.ToList();

            Workers azonosito = null;

            if (id != null)
            {
                azonosito = _db.Workers.FirstOrDefault(w => w.Id == id);
            }

            
            return View(azonosito);
        }
        [HttpGet]
        public IActionResult Munkatekintes(int id)
        {
            var munka = _db.Jobs.FirstOrDefault(w => w.Id == id);

            if(munka == null)
            {
                return NotFound();
            }
            
            return View(munka);
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
            

            return View("ImageUpdate");
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteByName(string nev_delete)
        {
            if (string.IsNullOrWhiteSpace(nev_delete))
            {
                TempData["Error"] = "Add meg a törlendõ kép nevét!";
                return RedirectToAction("Index"); // vagy ahova vissza akarsz menni
            }

            // Itt feltételezem, hogy az Image táblában van egy Név mezõd.
            // (pl. Name, Nev, Title stb.) -> állítsd arra, ami nálad van!
            var img = _db.Images.FirstOrDefault(x => x.Nev == nev_delete);

            // Ha nem pontos egyezést akarsz, hanem kis/nagybetû függetlent:
            //var img = _db.Images.FirstOrDefault(x => x.Nev.ToLower() == nev_delete.ToLower());

            if (img == null)
            {
                TempData["Error"] = $"Nem található ilyen nevû kép: {nev_delete}";
                return RedirectToAction("ImageUpdate");
            }

            // Fájl törlés a wwwroot alól
            if (!string.IsNullOrEmpty(img.FilePath))
            {
                var physicalPath = Path.Combine(
                    _env.WebRootPath,
                    img.FilePath.TrimStart('/', '\\')
                );

                if (System.IO.File.Exists(physicalPath))
                    System.IO.File.Delete(physicalPath);
            }

            // DB rekord törlés
            _db.Images.Remove(img);
            _db.SaveChanges();

            TempData["Success"] = $"Sikeres törlés: {img.Nev}";
            return RedirectToAction("ImageUpdate");
        }

        public IActionResult ImageUpdate()
        {
            return View();
        }
       
//--------------------------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------------------------
       public IActionResult Calculator()
        {
            return View();
        }


        static List<string> calclista = new List<string>(); //calc. lista
        string asd2 = "";
        int ossz = 0;

        [HttpPost]
        public IActionResult Kiszamol(int kategoria, int terulet)
        {
            var kategoriak = new Dictionary<int, string>
    {
                {1, "Szobafestés"},
                {2, "Csempézés"},
                {3, "Parkettázás"},
                {4, "Tapétázás"},
                {5, "Burkolás, vakolás"},
                {7, "Vízvezeték javítás"}
                };
            var kateg = kategoriak.ContainsKey(kategoria) ? kategoriak[kategoria]
        : "Ismeretlen munka";

            
            int munkadij = 0;
            
            int anyagar = 0;



            switch (kategoria)
            {
                case 1:
                    munkadij = 3000;
                    anyagar = 600;
                    ossz = munkadij * terulet + anyagar * terulet;
                    break;

                case 2:
                    munkadij = 6000;
                    anyagar = 7500;
                    ossz = munkadij * terulet + anyagar * terulet;
                    break;

                case 3:
                    munkadij = 4500;
                    anyagar = 8000;
                    ossz = munkadij * terulet + anyagar * terulet;
                    break;

                case 4:
                    munkadij = 5000;
                    anyagar = 3000;
                    ossz = munkadij * terulet + anyagar * terulet;
                    break;

                case 5:
                    munkadij = 6000;
                    anyagar = 4000;
                    ossz = munkadij * terulet + anyagar * terulet;
                    break;

                case 7:
                    munkadij = 5000;
                    ossz = munkadij * terulet;

                    break;
            }
            ViewBag.Osszeg = ossz;
             asd2 = kateg+" "+terulet+"m2"+" "+ossz+" Ft";
            calclista.Add(asd2);
            return View("Calculator");
        }

        public IActionResult Letoltes()
        {
            return View(calclista);
        }

        public IActionResult listazas()
        {
            var listaelem = calclista.ToList();

            var szovegBuilder = new StringBuilder();

            szovegBuilder.AppendLine("A lista tartalma: ");
            szovegBuilder.AppendLine(" ");

            foreach (var item in listaelem)
            {
                szovegBuilder.AppendLine(item);
            }

            var bytes = Encoding.UTF8.GetBytes(szovegBuilder.ToString());

            return File(
                bytes,              // fájl tartalma
                "text/csv",         // típus
                "munkalista.csv"  // fájl neve
                );
        }
        //--------------------------------------------------------------------------------------------------------
        public IActionResult Gallery()
        {
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            var image = _db.Images.ToList(); // List<Image>

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var allowedExt = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            var images = Directory.EnumerateFiles(uploadsPath)
                .Where(f => allowedExt.Contains(Path.GetExtension(f)))
                .Select(f => "/uploads/" + Path.GetFileName(f))  // URL a wwwroot-on belül
                .OrderByDescending(url => System.IO.File.GetLastWriteTime(
                    Path.Combine(uploadsPath, Path.GetFileName(url))
                ))
                .ToList();
            
            return View(image);

            return View(images);
        }


        public IActionResult Ertekeles()
        {
            ViewBag.Atlag = _db.Ratings.Average(x => x.Ertek);

        
            return View();
        }

        public int osszPont = 0;
        [HttpPost]
        public IActionResult Ertekel(int pont, string leiras, string email)
        {
            if(pont==null || leiras== null|| email==null)
            {
                return BadRequest("Hiányzó adatok");
            }
            Ertekeles e = new Ertekeles
            {
                Ertek = pont,
                Desc = leiras,
                Email = email
            };
            _db.Ratings.Add(e);
            _db.SaveChanges();
            osszPont += pont;


            return RedirectToAction("Ertekeles");
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
