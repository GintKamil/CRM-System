using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Modules.Auth.Infrastructure.Mail;
using CRMSystem.Modules.Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace CRMSystem.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class RegisterController : Controller
    {
        private IUserService _userService;
        private readonly IAuthService _authService;

        public RegisterController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/Auth/Views/Register/Register.cshtml");
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                model.Password = await _userService.PasswordHashing(model.Password);

                await _userService.Create(model);

                TempData["name"] = model.Name;
                TempData["email"] = model.Email;

                var identity = await _authService.AuthenticateAsync(model.Email);

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

                return RedirectToAction("CheckEmail", "Authorization");
            }
            return View(model);
        }
    }
}
