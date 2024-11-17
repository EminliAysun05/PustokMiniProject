using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustokk.BLL.ViewModels.AppUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pustokk.BLL.Services.Contracts
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState);
        Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState);
       // Task<bool> SignOutAsync();
       // Task<bool> VerifyEmailAsync(string email, string token);
       // Task<bool> SendResetPasswordLinkAsync(string email);

    }
}
