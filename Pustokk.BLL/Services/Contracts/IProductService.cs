using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.PaginateViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace Pustokk.BLL.Services.Contracts;

public interface IProductService 
{
    // Task<ProductViewModel> GetByCategoryIdAsync(int categoryId);
    List<ProductViewModel> SearchProducts(string query);
    Task<ProductPaginateViewModel> GetPaginatedProductAsync(int pageIndex, int pageSize, string sortBy);
    Task<List<ProductViewModel>> GetBestSellingProductsAsync();
    Task<List<ProductViewModel>> GetRelatedProductAsync(int productId);
    Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId);
    Task<List<ProductViewModel>> GetByCategoryIdAsync(int categoryId);
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
