using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class Shop : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
