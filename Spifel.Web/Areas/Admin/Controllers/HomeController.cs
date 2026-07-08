using Microsoft.AspNetCore.Mvc;

namespace Spifel.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return Content("Admin area");
        }
    }
}
