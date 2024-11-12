using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.BLL.ViewModels.CategoryViewModels;

namespace Pustokk.BLL.Services.Contracts;

public interface IProductService 
{
    Task<ProductViewModel> DeleteAsync(int id);
    Task<ProductViewModel?> GetAsync(int id);
    Task<List<CategoryViewModel>> GetCategoriesAsync();
    Task<List<TagViewModel>> GetTagsAsync();
    Task<ProductViewModel> AddProductWithImagesAsync(ProductCreateViewModel createViewModel);
    Task<ProductViewModel> UpdateAsync(ProductUpdateViewModel updateViewModel);
    Task<List<ProductViewModel>> GetAllAsync(
    Expression<Func<Product, bool>>? predicate = null,
    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null);
}
