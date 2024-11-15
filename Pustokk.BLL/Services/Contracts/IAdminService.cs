using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.BLL.Services.Contracts
{
    public interface IAdminService
    {
        Task<List<AppUser>> GetAllUserAsync();
        Task<bool> ChangeUserRoleAsync(string userId, string newRole);
        Task<bool> UseerActivationAsync(string userId, bool isActive);
    }
}
