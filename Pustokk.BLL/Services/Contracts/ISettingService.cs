using Pustokk.BLL.ViewModels.SetttingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface ISettingService
    {
        Task<List<SettingViewModel>> GetAllAsync();
        Task<SettingViewModel?> GetAsync(int id);
        Task<SettingViewModel> CreateAsync(SettingCreateViewModel model);
        Task<SettingViewModel> UpdateAsync(SettingUpdateViewModel model);
        Task<SettingViewModel> DeleteAsync(int id);
    }
}
