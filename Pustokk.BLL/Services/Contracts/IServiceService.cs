using Pustokk.BLL.ViewModels.ServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface IServiceService
    {
         Task<List<ServiceViewModel>> GetAllAsync();
        Task<ServiceViewModel?> GetAsync(int id);
        Task<ServiceViewModel> DeleteAsync(int id);
        Task<ServiceViewModel> UpdateAsync(ServiceUpdateViewModel model);
        Task<ServiceViewModel> CreateAsync(ServiceCreateViewModel model);
       
    }
}
