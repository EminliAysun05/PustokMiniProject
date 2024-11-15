using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Pustokk.BLL.Services.Contracts;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{
    public class AdminService : IAdminService
    {

        private readonly UserManager<AppUser> _userManager;
      

        public AdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
          
        }

        public async Task<List<AppUser>> GetAllUserAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles != null && currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles.ToList());
                if (!removeResult.Succeeded) return false;
            }

            var addResult = await _userManager.AddToRoleAsync(user, newRole);

            return addResult.Succeeded;
        }




        public async Task<bool> UseerActivationAsync(string userId, bool isActive)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            user.IsActive = isActive;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;

        }
    }
}
