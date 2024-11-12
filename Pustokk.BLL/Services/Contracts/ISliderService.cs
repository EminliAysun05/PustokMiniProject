using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.ViewModels.SliderViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{
    public interface ISliderService
    {
        Task<List<SliderViewModel>> GetAllAsync(
    Expression<Func<Slider, bool>>? predicate = null,
    Func<IQueryable<Slider>, IIncludableQueryable<Slider, object>>? include = null,
    Func<IQueryable<Slider>, IOrderedQueryable<Slider>>? orderBy = null);
        Task<SliderViewModel> CreateAsync(SliderCreateViewModel createViewModel);
        Task<SliderViewModel> UpdateAsync(SliderUpdateViewModel updateViewModel);
        Task<SliderViewModel> DeleteAsync(int id);
        Task<SliderViewModel> GetAsync(int id);
    }
}
