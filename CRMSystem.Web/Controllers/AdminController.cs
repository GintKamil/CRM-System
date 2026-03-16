using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Shared.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleDto dto)
        {
            Console.WriteLine(dto.Role);
            await _userService.UpdateUserRole(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
