using Microsoft.AspNetCore.Mvc;

namespace SK_PG_WebApp.Controllers
{
    public class WebSiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }
    }
}
