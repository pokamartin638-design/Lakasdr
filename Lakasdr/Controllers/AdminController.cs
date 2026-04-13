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
            return RedirectToAction("Index", "Home");
        }
        //-------------------------------------------------------------
        public IActionResult WorkersUpdate()
        {
            var munkatarsak = _db.Workers
                     .Include(w => w.Jobs)
                     .ToList();
            return View(munkatarsak);
        }
        [HttpGet]
        public IActionResult WorkersSzerkesztes(int id)
        {
            var worker = _db.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            ViewBag.Jobs = _db.Jobs.ToList();
            return View(worker);
        }
        [HttpPost]
        public IActionResult WorkersSzerkesztes(Workers worker)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Jobs = _db.Jobs.ToList();
                return View(worker);
            }

            var letezoWorker = _db.Workers.FirstOrDefault(w => w.Id == worker.Id);
            if (letezoWorker == null)
            {
                return NotFound();
            }

            var letezoMunka = _db.Jobs.Any(j => j.Id == worker.WorkId);
            if (!letezoMunka)
            {
                ModelState.AddModelError("WorkId", "A kiválasztott munka nem létezik.");
                ViewBag.Jobs = _db.Jobs.ToList();
                return View(worker);
            }

            letezoWorker.Name = worker.Name;
            letezoWorker.WorkId = worker.WorkId;
            letezoWorker.Exp = worker.Exp;

            _db.SaveChanges();

            return RedirectToAction("WorkersUpdate");
        }
        [HttpGet]
        public IActionResult NewWorkers()
        {
            ViewBag.Jobs = _db.Jobs.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult NewWorkers(Workers worker)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Jobs = _db.Jobs.ToList();
                return View(worker);
            }

            var letezoMunka = _db.Jobs.Any(j => j.Id == worker.WorkId);
            if (!letezoMunka)
            {
                ModelState.AddModelError("WorkId", "A kiválasztott munka nem létezik.");
                ViewBag.Jobs = _db.Jobs.ToList();
                return View(worker);
            }

            var dolgozo = new Workers
            {
                Name = worker.Name,
                WorkId = worker.WorkId,
                Exp = worker.Exp
            };

            _db.Workers.Add(dolgozo);
            _db.SaveChanges();

            return RedirectToAction("WorkersUpdate");
        }

        [HttpPost]
        public IActionResult DeleteWorkers(int id)
        {
            var torlendo = _db.Workers.FirstOrDefault(x =>x.Id == id);

            if(torlendo != null)
            {
                _db.Workers.Remove(torlendo);
                _db.SaveChanges();
            }
            return RedirectToAction("WorkersUpdate");
        }

        public IActionResult Jobs()
        {
            var munkak = _db.Jobs.ToList();
            return View(munkak);
        }
        [HttpGet]
        public IActionResult JobsSzerkesztes(int id)
        {
            var job = _db.Jobs.FirstOrDefault(x => x.Id == id);

            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }
        [HttpPost]
        public IActionResult JobsSzerkesztes(Jobs job)
        {
            _db.Jobs.Update(job);
            _db.SaveChanges();
            return RedirectToAction("Jobs");
        }
        public IActionResult NewJobs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewJobs(Jobs job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }
            var melo = new Jobs
            {
                Id = job.Id,
                Name = job.Name,
                Description = job.Description
            };
            _db.Jobs.Add(melo);
            _db.SaveChanges();

            return RedirectToAction("Jobs");
        }
        [HttpPost]
        public IActionResult DeleteJobs(int id)
        {
            var torlendo = _db.Jobs.FirstOrDefault(x => x.Id == id);

            if (torlendo != null)
            {
                _db.Jobs.Remove(torlendo);
                _db.SaveChanges();
            }
            return RedirectToAction("Jobs");
        }

        //-----------------------------------------------------------------------------
        //velemenyek torlese
        public IActionResult AdminRating()
        {
            var velemenyek = _db.Ratings
                .OrderByDescending(x => x.Ideje)
                .ToList();

            return View(velemenyek);
        }



        [HttpPost]
        public IActionResult RatingTorles(int id)
        {
            var torlendo = _db.Ratings.FirstOrDefault(x => x.Id == id);

            if (torlendo != null)
            {
                _db.Ratings.Remove(torlendo);
                _db.SaveChanges();
            }

            return RedirectToAction("AdminRating");
        }

    }
}
