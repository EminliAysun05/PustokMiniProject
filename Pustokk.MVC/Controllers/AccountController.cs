using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.AppUserViewModels;

namespace Pustokk.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;


        public AccountController(IAccountService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var result = await _service.RegisterAsync(vm, ModelState); //register

            if (result == false)
                return View(vm);

            TempData["Message"] = "Verification link sent to your email.";
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _service.LoginAsync(vm, ModelState);

            if (result == false) return View(vm);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            var result = await _service.SignOutAsync();


            if (result is false)
                return BadRequest();

            return RedirectToAction("Index", "Home");

        }

        //public IActionResult ResetPasswordRequest()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPasswordRequest(string email)
        //{
        //    var result = await _service.SendResetPasswordLinkAsync(email);

        //    if (!result)
        //    {
        //        ModelState.AddModelError("", "Failed to send link.");
        //    }

        //    TempData["Message"] = "Reset password link sent to your email";
        //    return RedirectToAction("Login");
        //}

        //public IActionResult ResetPassword(string token, string email)
        //{
        //    ViewBag.Token = token;
        //    ViewBag.Email = email;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        //{
        //    var result = await _service.ResetPasswordAsync(vm);

        //    if (!result)
        //    {
        //        ModelState.AddModelError("", "Failed to reset password.");
        //        return View(vm);
        //    }

        //    TempData["Message"] = "Password reset successfully.";
        //    return RedirectToAction("Login");
        //}

        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var result = await _service.VerifyEmailAsync(email, token);

            if (!result)
            {
                TempData["Error"] = "Email verification failed.";
                return RedirectToAction("Login");
            }

            TempData["Message"] = "Email verified successfully.";
            return RedirectToAction("Login");
        }
    }
}
