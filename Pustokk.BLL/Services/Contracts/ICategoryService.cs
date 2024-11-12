using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface ICategoryService 
    {
        Task<List<CategoryViewModel>> GetParentCategoriesAsync();
        Task CreateAsync(CategoryCreateViewModel model);
        Task<CategoryViewModel> UpdateAsync(CategoryUpdateViewModel model);
        Task<CategoryViewModel> GetAsync(int id);
        Task<CategoryViewModel> DeleteAsync(int id);
    }
}
