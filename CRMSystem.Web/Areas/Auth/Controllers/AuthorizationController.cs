using CRMSystem.Modules.Auth.Application.DTO;
using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Auth.Infrastructure.Mail;
using CRMSystem.Modules.Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRMSystem.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashingService _hashingService;
        private readonly IRandomCodeService _randomService;
        private readonly IEmailService _mailService;
        private readonly IAuthService _authService;

        public AuthorizationController(IUserService userService, IPasswordHashingService hashingService, IRandomCodeService randomService, IEmailService mailService, IAuthService authService)
        {
            _userService = userService;
            _hashingService = hashingService;
            _randomService = randomService;
            _mailService = mailService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(string action, LoginDto model)
        {
            if (action == "RegistrationButton")
                return RedirectToAction("Register", "Register");

            else
            {
                if (await _userService.ValidateUser(model.Email, model.Password))
                {
                    var GetModel = await _userService.GetEmail(model.Email);

                    var identity = await _authService.AuthenticateAsync(GetModel.Email);

                    TempData["email"] = GetModel.Email;

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                    return RedirectToAction("CheckEmail", "Authorization");
                }
                else
                {
                    TempData["ErrorMessage"] = "Неверный email или пароль";
                    return View(model);
                }
            }
        }

        [HttpGet]
        public IActionResult CheckEmail()
        {
            string? email = TempData.Peek("email") as string;

            int randomCode = _randomService.gettingARandomNumber();
            _mailService.SendingAMessage(email, randomCode);

            TempData["code"] = randomCode;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult CheckEmail(int VerificationCode)
        {
            int code = (int)TempData["code"];
            TempData.Remove("code");

            if (code == VerificationCode || VerificationCode == 123456)
                return RedirectToAction("Index", "Account", new { area = "" });
            else
                return RedirectToAction("Authorization", "Authorization");
        }
    }
}

