using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.MVC.Models;
using System.Diagnostics;

namespace Pustokk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            // View-a gönd?ririk
            return View(products);
        }
           public IActionResult Details(string name)
        {
            return View(model : name);
        }
       
    }
}
