using Pustokk.BLL.ViewModels.TagViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface ITagService
    {
        Task<List<TagViewModel>> GetAllAsync();
        Task<TagViewModel?> GetAsync(int id);
        Task<TagViewModel> CreateAsync(TagCreateViewModel model);
        Task<TagViewModel> UpdateAsync(TagUpdateViewModel model);
        Task<TagViewModel> DeleteAsync(int id);
    }
}
