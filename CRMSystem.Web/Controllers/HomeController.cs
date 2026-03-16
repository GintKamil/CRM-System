using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
