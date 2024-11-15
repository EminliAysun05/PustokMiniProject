using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.UI.Services;
using Pustokk.MVC.Models;
using System.Diagnostics;

namespace Pustokk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var viewModel = await _homeService.GetHomeViewModelAsync(categoryId);
            return View(viewModel);
        }
        //public async  Task<IActionResult> Details(int id)
        //{
        //   var product = await _homeService.GetProductDetailsAsync(id);

        //    if(product == null) return NotFound();

        //    return View(product);
        //}

    }
}
