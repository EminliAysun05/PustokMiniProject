using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModel;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.Repositories;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.UI.Services.Contracts
{
    public class HomeManager : IHomeService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISliderService _sliderService;
        private readonly IServiceService _serviceService;
        //  private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;
        // private readonly ILayoutService _layoutService;

        public HomeManager(IProductService productService, ICategoryService categoryService, ISliderService sliderService, IServiceService serviceService, AppDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
            _serviceService = serviceService;
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetBestSellingProductAsync()
        {
            var bestSellingProducts = await _context.Products.Take(10).
                Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DisCountPrice = p.DisCountPrice,
                    ImageUrls = p.ProductImages.Select(i => i.ImageUrl).ToList(),
                    CategoryName = p.Category.Name
                })
            .ToListAsync();

            return bestSellingProducts;
        }

        //private readonly ILayoutService _layoutService;

        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId)
        {

            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var sliders = await _sliderService.GetAllAsync();
            // var settings = await _layoutService.GetSettingsAsync();
            var services = await _serviceService.GetAllAsync();
            List<ProductViewModel> selectedProducts = new();

            if (categoryId is not null)
                selectedProducts = await _productService.GetByCategoryIdAsync((int)categoryId);

            else
                selectedProducts = await _productService.GetByCategoryIdAsync(categories.First().Id);


            return new HomeViewModel
            {
                Products = products,
                Categories = categories,
                Sliders = sliders,
                //Settings = settings,
                Services = services,
                SelectedProducts = selectedProducts,

            };
        }

        //public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId)
        //{
        //    var product = await _productService.GetAll()
        //         .Where(p => p.Id == productId)
        //         .Select(p => new ProductDetailsViewModel
        //         {
        //             Id = p.Id,
        //             Name = p.Name,
        //             Price = p.Price,
        //             Description = p.Description,
        //             ImageUrls = p.Images.Select(img => img.Url).ToList(),
        //             CategoryName = p.Category.Name
        //         })
        //         .FirstOrDefaultAsync();

        //    return product;
        //}

        //public async Task<IActionResult> ProductsByCategory(int categoryId)
        //{
        //    var products = await _productService.Get
        //}
    }
}
