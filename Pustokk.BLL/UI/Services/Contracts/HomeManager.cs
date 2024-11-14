using Pustokk.BLL.Services;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModel;
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
       // private readonly ILayoutService _layoutService;

        public HomeManager(IProductService productService, ICategoryService categoryService, ISliderService sliderService, IServiceService serviceService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
            _serviceService = serviceService;
        }

        //private readonly ILayoutService _layoutService;

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var sliders = await _sliderService.GetAllAsync();
           // var settings = await _layoutService.GetSettingsAsync();
           var services  = await _serviceService.GetAllAsync();

            return new HomeViewModel
            {
                Products = products,
                Categories = categories,
                Sliders = sliders,
                //Settings = settings,
               Services = services
            };
        }
    }
}
