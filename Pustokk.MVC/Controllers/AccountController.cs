using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Exceptions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.AppUserViewModels;
using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;



        public AccountController(IAccountService service, IAccountService accountService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _service = service;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel vm)
        //{
        //    var result = await _service.RegisterAsync(vm, ModelState); //register

        //    if (result == false)
        //        return View(vm);

        //    TempData["Message"] = "Verification link sent to your email.";
        //    return RedirectToAction("Login");
        //}

        public IActionResult Register()

        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _service.RegisterAsync(vm, ModelState);

            if (result == false)
                return View(vm);//bura dusur debugda

            // TempData["SuccessMessage"] = "Registration successful! Please log in.";

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
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _service.LoginAsync(vm, ModelState);

            if (result == false)
            {
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            try
            {
                var result = await _accountService.VerifyEmailAsync(email, token);

                if (result)
                {
                    TempData["Message"] = "Your email has been verified successfully!";
                    return RedirectToAction("Index", "Home");
                }

                TempData["Error"] = "Email verification failed.";
                return RedirectToAction("Login");
            }
            catch (InvalidInputException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Login");
            }
        }


        public async Task<IActionResult> LogOut()
        {
           await _service.LogOutAsync();


           

            return RedirectToAction("Login");

        }
    }


  
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
//bura bax
//public async Task<IActionResult> VerifyEmail(string token, string email)
//{
//    return RedirectToAction("Login");
//    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
//    {
//        return BadRequest("Invalid email confirmation request.");
//    }

//    var user = await _userManager.FindByEmailAsync(email);
//    if (user == null)
//    {
//        return NotFound("User not found.");
//    }

//    var result = await _userManager.ConfirmEmailAsync(user, token);

//    if (!result.Succeeded)
//    {
//        return BadRequest("Email confirmation failed.");
//    }

//    await _signInManager.SignInAsync(user, isPersistent: false);

//    TempData["SuccessMessage"] = "Your email has been successfully verified!";
//    return RedirectToAction("Index", "Home");
//}

//var result = await _service.VerifyEmailAsync(email, token);

//if (!result)
//{
//    TempData["Error"] = "Email verification failed.";
//    return RedirectToAction("Login");
//}

//TempData["Message"] = "Email verified successfully.";