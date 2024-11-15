using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustokk.BLL.Services.Contracts;

namespace Pustokk.MVC.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles ="Admin")]
public class UserController : Controller
{
   private readonly IAdminService _adminService;

    public UserController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _adminService.GetAllUserAsync();
        return View(users);
    }

    public async Task<IActionResult> ChangeUserRole(string userId, string newRole)
    {
        var result = await _adminService.ChangeUserRoleAsync(userId, newRole);

        if (!result)
        {
            TempData["Error"] = "Failed to change user role";
            return RedirectToAction("Index");
            //return Json(new { success = false, message = "Failed to change user role" });
        }

        TempData["Message"] = "User role succesfully changed";
        return RedirectToAction("Index");
        // return Json(new { success = true, message = "User role successfully changed" });
    }

    public async Task<IActionResult> UseerActivation(string userId, bool isActive)
    {
        var result = await _adminService.UseerActivationAsync(userId, isActive);

        if (!result)
        {
            TempData["Error"] = "Failed to change user activation";
            return RedirectToAction("Index");
        }

        TempData["Message"] = "User activation succesfully changed";
        return RedirectToAction("Index");
    }
}
