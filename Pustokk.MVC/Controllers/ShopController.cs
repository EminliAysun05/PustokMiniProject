using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModel;
using Pustokk.BLL.ViewModels;
using Pustokk.BLL.ViewModels.PaginateViewModels;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Pustokk.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Grid(int page = 1, int pageSize = 9, string? SortBy = null)
        {
            if (page <= 0) page = 1; 
            if (pageSize <= 0) pageSize = 9; 
            var products = await _productService.GetPaginatedProductAsync(page-1,pageSize, SortBy!);

            var model = new ProductPaginateViewModel
            {
                Products = products.Products,
                Index = products.Index,
                Size = products.Size,
                Count = products.Count,
                Pages = products.Pages,
                HasPrevious = products.HasPrevious,
                HasNext = products.HasNext,
                SortBy = SortBy,
                SortOptions = new Dictionary<string, string>
        {
            { "", "Default" },
            { "name-asc", "Name: A to Z" },
            { "name-desc", "Name: Z to A" },
            { "price-asc", "Price: Low to High" },
            { "price-desc", "Price: High to Low" }
        }
            };

            return View(model);
        }
    }
    
}
