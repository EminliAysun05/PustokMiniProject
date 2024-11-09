
using Microsoft.AspNetCore.Identity;
using Pustokk.DAL.DataContext.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.DataContext.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public RoleType UserRole { get; set; }

    }
}
