using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.DAL.DataContext;

namespace Pustokk.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(categoryViewModels);
        }

        public async Task<IActionResult> Create()
        {
          var categories = await _categoryService.GetParentCategoriesAsync();
            var categoryViewModel = new CategoryCreateViewModel
            {
                Categories = categories
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetParentCategoriesAsync();
                return View(model);
            }

            await _categoryService.CreateAsync(model); 
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var parentCategories = await _categoryService.GetParentCategoriesAsync();
            var viewModel = new CategoryUpdateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                Categories = parentCategories
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetParentCategoriesAsync();
                return View(model);
            }

            await _categoryService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _categoryService.DeleteAsync(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }

   
}
