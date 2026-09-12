using Microsoft.AspNetCore.Mvc;

namespace Spifel.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
