using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ContactViewModel;

namespace Pustokk.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMail(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model); // Əgər validasiya uğursuzdursa, form səhifəsinə qaytar
            }

            // E-poçt göndərilməsi üçün məzmun yaradılır
            var subject = $"New Contact Form Submission from {model.Name}";
            var body = $"<p><strong>Name:</strong> {model.Name}</p>" +
                       $"<p><strong>Email:</strong> {model.Email}</p>" +
                       $"<p><strong>Message:</strong></p>" +
                       $"<p>{model.Message}</p>";

            var toEmail = "aeminli232gmail.com"; // Buraya öz e-poçt ünvanınızı yazın

            // E-poçt göndər
            try
            {
                // Synchronous e-poçt göndərmə
                //_emailService.SendEmail(toEmail, subject, body);

                TempData["Message"] = "Your message has been sent successfully!";
                return RedirectToAction("ThankYou"); // Uğurlu olduqda "Thank You" səhifəsinə yönləndir
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                ModelState.AddModelError("", "There was an error sending your message. Please try again.");
                return View("Index", model);
            }
        }

        // Thank You Page
        public IActionResult ThankYou()
        {
            return View(); // "Thank You" səhifəsi göstərilir
        }
    }
}
    

