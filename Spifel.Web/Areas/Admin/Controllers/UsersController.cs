using Microsoft.AspNetCore.Mvc;
using Spifel.Application.Services.Interfaces;
using Spifel.Web.Areas.Admin.Feature.Users.UsersList;

namespace Spifel.Web.Areas.Admin.Controllers
{
    public class UsersController(IUserService _userService) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetAllUsersAsync();
            return View(model.Value.ToUsersListVM());
        }
    }
}
