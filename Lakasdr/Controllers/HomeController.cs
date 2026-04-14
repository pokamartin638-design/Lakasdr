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
            ViewBag.Workers = _db.Workers
                .Include(w => w.Jobs)
                .OrderBy(w => w.Name)
                .ToList();

            Workers azonosito = null;

            if (id != null)
            {
                azonosito = _db.Workers
                    .Include(w => w.Jobs)
                    .FirstOrDefault(w => w.Id == id);
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
            var images = _db.Images
                .OrderByDescending(x => x.UploadDate)
                .ToList();

            return View(images);
        }
        

        // UPLOAD POST
        [HttpPost]
        public IActionResult Upload(string nev, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Nem választottál fájlt.";
                return RedirectToAction("ImageUpdate");
            }

            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
            {
                TempData["Error"] = "Csak jpg, jpeg és png fájl tölthetõ fel.";
                return RedirectToAction("ImageUpdate");
            }

            if (string.IsNullOrWhiteSpace(nev))
            {
                TempData["Error"] = "Add meg a kép nevét is.";
                return RedirectToAction("ImageUpdate");
            }

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
                Nev = nev.Trim(),
                FilePath = "/uploads/" + fileName,
                UploadDate = DateTime.Now
            };

            _db.Images.Add(image);
            _db.SaveChanges();

            TempData["Success"] = "A kép sikeresen feltöltve.";
            return RedirectToAction("ImageUpdate");
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteByName(string nev_delete)
        {
            if (string.IsNullOrWhiteSpace(nev_delete))
            {
                TempData["Error"] = "Add meg a törlendõ kép nevét!";
                return RedirectToAction("ImageUpdate"); 
            }

           
            var img = _db.Images.FirstOrDefault(x => x.Nev == nev_delete);

           

            if (img == null)
            {
                TempData["Error"] = $"Nem található ilyen nevû kép: {nev_delete}";
                return RedirectToAction("ImageUpdate");
            }

          
            if (!string.IsNullOrEmpty(img.FilePath))
            {
                var physicalPath = Path.Combine(
                    _env.WebRootPath,
                    img.FilePath.TrimStart('/', '\\')
                );

                if (System.IO.File.Exists(physicalPath))
                    System.IO.File.Delete(physicalPath);
            }

         
            _db.Images.Remove(img);
            _db.SaveChanges();

            TempData["Success"] = $"Sikeres törlés: {img.Nev}";
            return RedirectToAction("ImageUpdate");
        }

        public IActionResult ImageUpdate()
        {
            var images = _db.Images
                .OrderByDescending(x => x.UploadDate)
                .ToList();

            return View(images);
        }
       
//--------------------------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------------------------
       public IActionResult Calculator()
        {
            return View();
        }


        static List<string> calclista = new List<string>(); //calc. lista
       
   

        [HttpPost]
     
        public IActionResult Kiszamol(int kategoria, int? terulet, int? munkaDb, int? vizTipus)
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

            var kateg = kategoriak.ContainsKey(kategoria)
                ? kategoriak[kategoria]
                : "Ismeretlen munka";

            int ossz = 0;
            string leiras = "";

            if (kategoria == 7)
            {
                if ( munkaDb > 0  )
                {
                    int alapAr = 0;
                    string tipusNev = "";

                    switch (vizTipus)
                    {
                        case 1:
                            alapAr = 5000;
                            tipusNev = "Csap javítás";
                            break;
                        case 2:
                            alapAr = 10000;
                            tipusNev = "Csõcsere";
                            break;
                        case 3:
                            alapAr = 12000;
                            tipusNev = "Duguláselhárítás";
                            break;
                        default:
                            alapAr = 5000;
                            tipusNev = "Ismeretlen";
                            break;
                    }

                    int db = munkaDb ?? 1;
                    ossz = db * alapAr;

                    leiras = $"{kateg} - {tipusNev} ({db} db)";
                }
                else
                {
                    ViewBag.Hiba = "Adjon meg legalább 1 darabot!";
                    return View("Calculator");
                }
        
                
            }
            else
            {
                if ( terulet > 0)
                {
                    int munkadij = 0;
                    int anyagar = 0;
                    int m2 = terulet ?? 0;

                    switch (kategoria)
                    {
                        case 1:
                            munkadij = 3000;
                            anyagar = 600;
                            break;
                        case 2:
                            munkadij = 6000;
                            anyagar = 7500;
                            break;
                        case 3:
                            munkadij = 4500;
                            anyagar = 8000;
                            break;
                        case 4:
                            munkadij = 5000;
                            anyagar = 3000;
                            break;
                        case 5:
                            munkadij = 6000;
                            anyagar = 4000;
                            break;
                    }

                    ossz = m2 * (munkadij + anyagar);
                    leiras = $"{kateg} - {m2} m2";
                }
                else
                {
                    ViewBag.Hiba = "Adjon meg legalabb 1 négyzetmétert!";
                    return View("Calculator");
                }
                   
            }

            // lista mentés
            string asd2 = $"{leiras} - {ossz} Ft";
            calclista.Add(asd2);
            ViewBag.Osszeg = ossz;
         

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
                // pl: "Vízvezeték javítás - Csap javítás (2 db) - 10000 Ft"
                var parts = item.Split(" - ");

                if (parts.Length >= 3)
                {
                    szovegBuilder.AppendLine($"{parts[0]};{parts[1]};{parts[2]}");
                }
                else
                {
                    szovegBuilder.AppendLine(item);
                }
            }

            var bytes = Encoding.UTF8.GetPreamble()
            .Concat(Encoding.UTF8.GetBytes(szovegBuilder.ToString()))
            .ToArray();

            return File(
                bytes,              // fájl tartalma
                "text/csv",         // típus
                "munkalista.csv"  // fájl neve
                );
        }
        //--------------------------------------------------------------------------------------------------------
        public IActionResult Gallery()
        {
            var images = _db.Images
                .OrderByDescending(i => i.UploadDate)
                .ToList();

            return View(images);
        }

        //-----------------------------------------------------------------------------------------
        public IActionResult Ertekeles()
        {
            ViewBag.Atlag = _db.Ratings.Any()
                ? Math.Round(_db.Ratings.Average(x => x.Ertek), 1)
                : 0;

            var velemenyek = _db.Ratings
                .OrderByDescending(x => x.Ideje)
                .ToList();

            return View(velemenyek);
        }

        [HttpPost]
        public IActionResult Ertekel(int pont, string leiras, string email)
        {
            if (pont == 0 || string.IsNullOrEmpty(leiras) || string.IsNullOrEmpty(email))
            {
                TempData["Hiba"] = "Hiányzó adatok!";
                return RedirectToAction("Ertekeles");
            }

            var letezo = _db.Ratings.FirstOrDefault(x => x.Email == email);

            if (letezo != null)
            {
                
                letezo.Ertek = pont;
                letezo.Desc = leiras;
                letezo.Ideje = DateTime.Now;
            }
            else
            {
                Ertekeles e = new Ertekeles
                {
                    Ertek = pont,
                    Desc = leiras,
                    Email = email,
                    Ideje = DateTime.Now
                };
                _db.Ratings.Add(e);
            }
            _db.SaveChanges();

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
