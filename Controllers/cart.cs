using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
