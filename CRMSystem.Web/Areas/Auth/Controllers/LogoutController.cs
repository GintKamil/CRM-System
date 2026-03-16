using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class LogoutController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Authorization", "Authorization");
        }
    }
}
