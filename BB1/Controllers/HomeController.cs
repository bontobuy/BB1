using BB1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BontoOne.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(User model)
        {
            if (ModelState.IsValid)
            {
                var randomNumber = ActivationCode();
                var body = "<p>Dear Customer,</p><p>This is the activation code that has been sent to you in order to validate your registration on BontoBuy</p><p>Your activation code: {0}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.Email)); //The mail of the person registering on the website
                message.From = new MailAddress("bontobuy@gmail.com");
                message.Subject = "Register on BontoBuy";
                message.Body = string.Format(body, randomNumber);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "bontobuy@gmail.com",
                        Password = "b0nt0@dmin"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent", "Home");

                }

            }
            return View(model);

        }
        public ActionResult Sent()
        {
            return View();
        }


        public static string ActivationCode()
        {
            int length = 8;
            const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var RandomNumber = new Random();
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[RandomNumber.Next(s.Length)]).ToArray());
        }

    }
}