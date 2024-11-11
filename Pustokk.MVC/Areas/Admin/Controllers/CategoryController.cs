using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.DAL.DataContext;

namespace Pustokk.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(categoryViewModels);
        }

        public async Task<IActionResult> Create()
        {
          

            return View();
        }

    }
}
