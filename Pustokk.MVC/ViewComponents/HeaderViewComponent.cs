using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services.Contracts;
using Pustokk.DAL.DataContext;
using Pustokk.MVC.ViewModels;

namespace Pustokk.MVC.ViewComponents
{
    public class HeaderViewComponent :ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var setting = await _dbContext.Settings.SingleOrDefaultAsync();

            var categories = await _dbContext.Categories.ToListAsync();

            // View üçün bir model yarat və məlumatları ötür
            var headerViewModel = new HeaderViewModel
            {
                Setting = setting,
                Categories = categories
            };

            return View(headerViewModel);
        }
    }
}
