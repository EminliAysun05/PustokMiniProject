using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.MVC.Areas.Admin.Controllers;

[Area("Admin")]
//[ValidateAntiForgeryToken]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly ICloudService _cloudService;
    private readonly IProductService _productService;

    public ProductController(AppDbContext context, ICloudService cloudService, IProductService productService)
    {
        _context = context;
        _cloudService = cloudService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        var viewModel = new ProductCreateViewModel
        {
            Categories = await _context.Categories.ToListAsync(),
            Tags = await _context.Tags.ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel vm)
    {
        vm.Categories = await _context.Categories.ToListAsync();
        vm.Tags = await _context.Tags.ToListAsync();

       
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        try
        {
           
            await _productService.AddProductWithImagesAsync(vm);

            
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            
            ModelState.AddModelError("", "An error occurred: " + ex.Message);
            return View(vm);
        }
    }
}




