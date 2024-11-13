using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.SetttingViewModels;

namespace Pustokk.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class SettingController : Controller
{
    private readonly ISettingService _settingService;
    private readonly IMapper _mapper;

    public SettingController(ISettingService settingService, IMapper mapper)
    {
        _settingService = settingService;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var settings = await _settingService.GetAllAsync();
        return View(settings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SettingCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _settingService.CreateAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var setting = await _settingService.GetAsync(id);
        if (setting == null)
            return NotFound();

        var updateModel = _mapper.Map<SettingUpdateViewModel>(setting);

        return View(updateModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(SettingUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _settingService.UpdateAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _settingService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}

