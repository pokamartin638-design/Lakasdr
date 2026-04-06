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
    public void Munkatekintes_MissingJob_ReturnsNotFound()
    {
        using var db = TestDbFactory.CreateContext(nameof(Munkatekintes_MissingJob_ReturnsNotFound));
        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Munkatekintes(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Munkatekintes_ExistingJob_ReturnsViewWithModel()
    {
        using var db = TestDbFactory.CreateContext(nameof(Munkatekintes_ExistingJob_ReturnsViewWithModel));
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
    public void Kiszamol_Category1Area10_SetsExpectedTotalAndReturnsCalculatorView()
    {
        using var db = TestDbFactory.CreateContext(nameof(Kiszamol_Category1Area10_SetsExpectedTotalAndReturnsCalculatorView));
        var env = new FakeWebHostEnvironment();
        var controller = new HomeController(db, env);

        var result = controller.Kiszamol(1, 10, 2, 5);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal("Calculator", view.ViewName);
        Assert.Equal(36000, controller.ViewBag.Osszeg);
    }

    [Fact]
    public void Ertekel_NewEmail_CreatesRatingAndRedirects()
    {
        using var db = TestDbFactory.CreateContext(nameof(Ertekel_NewEmail_CreatesRatingAndRedirects));
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
    public void Ertekel_ExistingEmail_UpdatesExistingRating()
    {
        using var db = TestDbFactory.CreateContext(nameof(Ertekel_ExistingEmail_UpdatesExistingRating));
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
    public void Upload_InvalidExtension_ReturnsViewAndDoesNotSaveImage()
    {
        using var db = TestDbFactory.CreateContext(nameof(Upload_InvalidExtension_ReturnsViewAndDoesNotSaveImage));
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
