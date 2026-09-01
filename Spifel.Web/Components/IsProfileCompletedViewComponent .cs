using Microsoft.AspNetCore.Mvc;
using Spifel.Application.Services.Interfaces;
using System.Security.Claims;
using Spifel.Web.Extensions;

namespace Spifel.Web.Components
{
    public class IsProfileCompletedViewComponent(IAccountService _accountService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _accountService.IsProfileCompletedAsync(HttpContext.User.GetId());

            return View(result.IsSuccess);
        }
    }
}
