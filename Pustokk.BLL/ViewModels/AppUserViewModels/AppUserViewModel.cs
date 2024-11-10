using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.AppUserViewModels
{
    public class AppUserViewModel : IViewModel
    {
    }
    public class LoginViewModel : IViewModel
    {
        public required string EmailOrUserName { get; set; }
        public required string Password { get; set; }
        public bool SaveMe { get; set; } = false;
    }

    public class RegisterViewModel : IViewModel
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public  required string Password { get; set; }
    }
}
