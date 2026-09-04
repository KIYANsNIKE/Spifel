using Microsoft.AspNetCore.Mvc;

namespace Spifel.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
