using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


public class EmailController : Controller
{
    [HttpPost]
    public IActionResult SendEmail(string email)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sajat App", "teemailcimed@gmail.com")); //kitol (sajat appot meg meg kell nezni mit jelent)
        message.To.Add(new MailboxAddress("", email));  //kinek
        message.Subject = "Teszt üzenet"; //email targya

        message.Body = new TextPart("plain")
        {
            Text = "Szia! Ez egy teszt email az MVC alkalmazásból."
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("teemailcimed@gmail.com", "APP_JELSZO"); //gmail fiokban kell 2faktoros azon, majd app passwordben kod kunyi
            client.Send(message);
            client.Disconnect(true);
        }

        return Content("Email elküldve!");
    }
}
