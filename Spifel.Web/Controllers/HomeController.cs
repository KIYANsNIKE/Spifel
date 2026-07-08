using Microsoft.AspNetCore.Mvc;

namespace Spifel.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
