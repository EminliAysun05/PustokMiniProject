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

        public async Task<IActionResult> Index()
        {
            var viewModel = await _homeService.GetHomeViewModelAsync();
            return View(viewModel);
        }
           public IActionResult Details(string name)
        {
            return View(model : name);
        }
       
    }
}
