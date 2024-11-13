using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ServiceViewModels;

namespace Pustokk.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
           _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _serviceService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var service = await _serviceService.GetAsync(id);
            if (service == null)
                return NotFound();

            var updateModel = new ServiceUpdateViewModel
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                ExistingIconUrl = service?.IconUrl
            };
            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _serviceService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
