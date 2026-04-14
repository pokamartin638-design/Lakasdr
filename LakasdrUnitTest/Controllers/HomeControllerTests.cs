using Lakasdr.Controllers;
using Lakasdr.Models;
using Lakasdr.Tests.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Lakasdr.Tests.Controllers;

public class HomeControllerTests
{
    [Fact]
    public void Munkatekintes_nemletezo()
    {
        using var db = TestDbFactory.CreateContext(nameof(Munkatekintes_nemletezo));
        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Munkatekintes(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Munkatekintes_letezo()
    {
        using var db = TestDbFactory.CreateContext(nameof(Munkatekintes_letezo));
        db.Jobs.Add(new Jobs { Id = 1, Name = "Festés", Description = "Belső szobafestés" });
        db.SaveChanges();

        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Munkatekintes(1);

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Jobs>(view.Model);
        Assert.Equal("Festés", model.Name);
    }

    [Fact]
    public void Kiszamol_helyes()
    {
        using var db = TestDbFactory.CreateContext(nameof(Kiszamol_helyes));
        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Kiszamol(1, 10, 2, 5);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal("Calculator", view.ViewName);
        Assert.Equal(36000, controller.ViewBag.Osszeg);
    }

    [Fact]
    public void Ertekel_letrehozas()
    {
        using var db = TestDbFactory.CreateContext(nameof(Ertekel_letrehozas));
        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Ertekel(5, "Nagyon jó munka", "teszt@example.com");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Ertekeles", redirect.ActionName);
        var rating = Assert.Single(db.Ratings);
        Assert.Equal(5, rating.Ertek);
        Assert.Equal("teszt@example.com", rating.Email);
    }

    [Fact]
    public void Ertekel_letezovaltoztatasa()
    {
        using var db = TestDbFactory.CreateContext(nameof(Ertekel_letezovaltoztatasa));
        db.Ratings.Add(new Ertekeles
        {
            Email = "teszt@example.com",
            Desc = "Régi értékelés",
            Ertek = 2,
            Ideje = DateTime.Now.AddDays(-2)
        });
        db.SaveChanges();

        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Ertekel(4, "Frissített értékelés", "teszt@example.com");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Ertekeles", redirect.ActionName);
        var rating = Assert.Single(db.Ratings);
        Assert.Equal(4, rating.Ertek);
        Assert.Equal("Frissített értékelés", rating.Desc);
    }

    [Fact]
    public void Upload_nemhelyestipus()
    {
        using var db = TestDbFactory.CreateContext(nameof(Upload_nemhelyestipus ));
        var env = new FakeWebHostEnvironment
        {
            WebRootPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
        };
        Directory.CreateDirectory(env.WebRootPath);

        var controller = new HomeController(db, env);
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        IFormFile file = new FormFile(stream, 0, stream.Length, "file", "test.txt");

        var result = controller.Upload("Minta", file);

        Assert.IsType<ViewResult>(result);
        Assert.Empty(db.Images);
    }
}
