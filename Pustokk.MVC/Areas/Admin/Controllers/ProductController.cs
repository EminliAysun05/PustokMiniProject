using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
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
    private readonly IMapper _mapper;

    public ProductController(AppDbContext context, ICloudService cloudService, IProductService productService, IMapper mapper)
    {
        _context = context;
        _cloudService = cloudService;
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _productService.GetCategoriesAsync();
        var tags = await _productService.GetTagsAsync();

        var productViewModel = new ProductCreateViewModel
        {
            Categories = await _context.Categories.ToListAsync(),
            Tags = await _context.Tags.ToListAsync()
        };

        return View(productViewModel);
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

   
    public async Task<IActionResult> Update(int id)
    {
        var product = await _productService.GetAsync(id);

        if(product == null)
        {
            return NotFound("Product not found");
        }

        var categories = await _productService.GetCategoriesAsync();
        var tags = await _productService.GetTagsAsync();

        var viewModel = _mapper.Map<ProductUpdateViewModel>(product);

        viewModel.Categories = categories ?? new List<CategoryViewModel>();
        viewModel.Tags = tags ?? new List<TagViewModel>();
        //viewModel.Categories = categories;
        //viewModel.Tags = tags;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProductUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _productService.GetCategoriesAsync();
            model.Tags = await _productService.GetTagsAsync();
            return View(model);
        }

        await _productService.UpdateAsync(model);
        return RedirectToAction("Index");
    }

    
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetAsync(id);
        if (product == null)
        {
            return NotFound("Məhsul tapılmadı");
        }

        await _productService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Details(int id)
    {
        var productDetails = await _productService.GetProductDetailsAsync(id);
        if(productDetails == null)
        {
            TempData["Error"] = "Product not found!";
            return RedirectToAction("Index");

        }

        return View(productDetails);
    }

    public IActionResult Search(string query)
    {
        var results = _productService.SearchProducts(query);
        return Json(results);
    }

}




