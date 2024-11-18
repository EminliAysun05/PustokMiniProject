using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json.Linq;
using Pustokk.BLL.Exceptions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.AppUserViewModels;
using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.BLL.Services;

public class AccountManager : IAccountService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;
    private readonly IUrlHelper _urlHelper;
    public AccountManager(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
    }

    //    public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
    //    {
    //        if (!modelState.IsValid)
    //            return false;

    //        var user = new AppUser
    //        {
    //            FullName = vm.FullName,
    //            UserName = vm.Email,
    //            Email = vm.Email
    //        };

    //        var result = await _userManager.CreateAsync(user, vm.Password);

    //        if (!result.Succeeded)
    //        {
    //            foreach (var error in result.Errors)
    //            {
    //                modelState.AddModelError("", error.Description);
    //            }
    //            return false;
    //        }

    //        await _signInManager.SignInAsync(user, isPersistent: false);
    //        return true;
    //    }

    //    public async Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState)
    //    {
    //        if (!modelState.IsValid)
    //            return false;

    //        var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: true, lockoutOnFailure: false);

    //        if (!result.Succeeded)
    //        {
    //            modelState.AddModelError("", "Invalid login attempt.");
    //            return false;
    //        }

    //        return true;
    //    }
    //}
    //}

    public async Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState)
    {
        if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            throw new InvalidInputException("User already signed");


        if (!modelState.IsValid)
            return false;

        var user = await _userManager.FindByEmailAsync(vm.EmailOrUserName);

        if (user is null)
            user = await _userManager.FindByNameAsync(vm.EmailOrUserName);

        if (user is null)
        {
            modelState.AddModelError("", "Email ve ya password yanlisdir");
            return false;
        }

        //if (!await _userManager.IsEmailConfirmedAsync(user))
        //{
        //    modelState.AddModelError("", "Email is not confirmed");
        //    return false;
        //}

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.SaveMe, true);

        if (!result.Succeeded)
        {
            modelState.AddModelError("", "Email ve y apassword yanlisdir");
            return false;
        }

        return true;

    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
    {
        if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            throw new InvalidInputException("User already signed");


        if (!modelState.IsValid)
            return false;

        var user = _mapper.Map<AppUser>(vm);
        var result = await _userManager.CreateAsync(user, vm.Password);

        //_emailService.SendEmail(vm.Email, "  ", "  ");

        if (!result.Succeeded)
        {
            modelState.AddModelError("", string.Join(", ", result.Errors.Select(x => x.Description)));
            return false;
        }
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);



        UrlActionContext context = new()
        {
            Action = "VerifyEmail",
            Controller = "Account",
            Values = new { token = token, email = user.Email },
            Protocol = _httpContextAccessor.HttpContext.Request.Scheme
        };

        var link = _urlHelper.Action(context);

        //user.Email!, "Verify your email","

        //bax user.Email!
        await _emailService.SendEmailAsync(new() { Body = $"Click here to verify your email: {link}", Subject = "Verify your email", ToEmail = user.Email });//anladim niye tesekkurlerrr  gozle hele yoxlayaq
        await _signInManager.SignInAsync(user, isPersistent: false);
        return true;

    }





    //public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
    //{
    //    if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
    //        throw new InvalidInputException("User already signed");


    //    if (!modelState.IsValid)
    //        return false;
    //    var user = _mapper.Map<AppUser>(vm);
    //    var result = await _userManager.CreateAsync(user, vm.Password);
    //    if (!result.Succeeded)
    //    {
    //        modelState.AddModelError("", string.Join(", ", result.Errors.Select(x => x.Description)));
    //        return false;
    //    }
    //    return true;

    //}

    //public async Task<bool> SignOutAsync()
    //{
    //    if (!_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
    //        return false;

    //    await _signInManager.SignOutAsync();
    //    return true;

    //}

    public async Task<bool> VerifyEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new InvalidInputException("User not found");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            throw new InvalidInputException("Invalid email verification ");

        await _signInManager.SignInAsync(user, false);

        return true;
    }
}
//public async Task<bool> SendResetPasswordLinkAsync(string email)
//{
//    var user = await _userManager.FindByEmailAsync(email);

//    if (user == null)
//        throw new InvalidInputException("User not found");

//    if (!await _userManager.IsEmailConfirmedAsync(user))
//        throw new InvalidInputException("Email is not confirmed");

//    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

//    var resetLink = $"https://yourwebsite.com/Account/ResetPassword?token={token}&email={user.Email}";


//    await _emailService.SendAsync(user.Email, "Reset Password", $"Click here to reset your password: {resetLink}");

//    return true;

//}

