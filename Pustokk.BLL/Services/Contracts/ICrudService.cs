using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.ViewModels;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.DAL.DataContext.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services.Contracts
{

    public interface ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
        where TEntity : BaseEntity
        where TViewModel : IViewModel
        where TCreateViewModel : IViewModel
        where TUpdateViewModel : IViewModel
    {

        Task<TViewModel?> GetAsync(int id);
        Task<TViewModel?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<List<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<TViewModel> CreateAsync(TCreateViewModel createViewModel);
        Task<TViewModel> UpdateAsync(TUpdateViewModel updateViewModel);
        Task<TViewModel> DeleteAsync(int id);
        
    }
}
