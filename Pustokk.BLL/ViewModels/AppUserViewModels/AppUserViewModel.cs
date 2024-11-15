using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.AppUserViewModels
{
    public class AppUserViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsActive { get; set; }
        public required string FullName { get; set; }
       // public DateTime RegistrationDate { get; set; }
    }
}
