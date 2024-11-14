using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.SliderViewModels;

namespace Pustokk.MVC.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View(new SliderCreateViewModel
            {
                Title = string.Empty, 
                Description = string.Empty,
                ButtonText = string.Empty,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _sliderService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var slider = await _sliderService.GetAsync(id);
            if (slider == null) return NotFound();

            var viewModel = _mapper.Map<SliderUpdateViewModel>(slider);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SliderUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _sliderService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

