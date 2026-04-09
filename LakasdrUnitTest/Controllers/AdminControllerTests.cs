using Lakasdr.Controllers;
using Lakasdr.Models;
using Lakasdr.Tests.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Lakasdr.Tests.Controllers;

public class AdminControllerTests
{
    [Fact]
    public void Sikeres_Belepes()
    {
        using var db = TestDbFactory.CreateContext(nameof(Sikeres_Belepes));
        var env = new FakeWebHostEnvironment();
        var controller = new AdminController(db, env);
        controller.TempData = new TempDataDictionary(
            new DefaultHttpContext(),   
            Mock.Of<ITempDataProvider>());

        var result = controller.Admin("admin", "1234");
            

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("AdminFelulet", redirect.ActionName);
    }

    [Fact]
    public void Sikertelen_Belepes()
    {
        using var db = TestDbFactory.CreateContext(nameof(Sikertelen_Belepes));
        var env = new FakeWebHostEnvironment();
        var controller = new AdminController(db, env);

        var result = controller.Admin("hibas", "jelszo");

        Assert.IsType<ViewResult>(result);
        Assert.Equal("error", controller.ViewBag.LoginStatus);
    }

    [Fact]
    public void WorkersSzerkesztes_nemeletezo()
    {
        using var db = TestDbFactory.CreateContext(nameof(WorkersSzerkesztes_nemeletezo));
        var env = new FakeWebHostEnvironment();
        var controller = new AdminController(db, env);

        var result = controller.WorkersSzerkesztes(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void NewWorkers_sikeresletrehozas()
    {
        using var db = TestDbFactory.CreateContext(nameof(NewWorkers_sikeresletrehozas));
        var env = new FakeWebHostEnvironment();
        var controller = new AdminController(db, env);

        var worker = new Workers
        {
            Name = "Teszt Elek",
            Exp = 5,
            WorkId = 1
        };

        var result = controller.NewWorkers(worker);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("WorkersUpdate", redirect.ActionName);
        var saved = Assert.Single(db.Workers);
        Assert.Equal("Teszt Elek", saved.Name);
    }

    [Fact]
    public void RatingTorles_ExistingRating_DeletesRatingAndRedirects()
    {
        using var db = TestDbFactory.CreateContext(nameof(RatingTorles_ExistingRating_DeletesRatingAndRedirects));
        db.Ratings.Add(new Ertekeles
        {
            Id = 10,
            Email = "teszt@example.com",
            Desc = "Törlendő",
            Ertek = 3,
            Ideje = DateTime.Now
        });
        db.SaveChanges();

        var env = new FakeWebHostEnvironment();
        var controller = new AdminController(db, env);

        var result = controller.RatingTorles(10);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("AdminRating", redirect.ActionName);
        Assert.Empty(db.Ratings);
    }
}
