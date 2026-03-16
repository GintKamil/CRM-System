using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string? name = User.FindFirst(ClaimTypes.Name)?.Value;
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            string? role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (name == null && email == null)
                return RedirectToAction("Authorization", "Authorization", new { area = "Auth" });
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Role = role;
            return View();
        }


        [HttpPost]
        public IActionResult Logout() =>
            RedirectToAction("Index", "Logout", new { area = "Auth" });
    }
}
