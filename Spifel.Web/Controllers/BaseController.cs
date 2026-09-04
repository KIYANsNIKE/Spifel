using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Spifel.Application.Services.Interfaces;
using Spifel.Domain.Models.User;
using Spifel.Web.Extensions;
using Spifel.Web.Feature.Account.Login;
using System.Security.Claims;

namespace Spifel.Web.Controllers
{
    public class BaseController() : Controller
    {
        protected async Task RefreshUserClaimsAsync()
        {
            var _accountService = HttpContext.RequestServices.GetRequiredService<IAccountService>();
            var userId = User.GetId();

            var result = await _accountService.GetUserByIdAsync(userId);
            var user = result.Value;
            if (user is null)
                return;

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,user.id.ToString()),
                new(ClaimTypes.Name,user.UserName),
                new("FullName",$"{user.FirstName} {user.LastName}"),
                new("Mobile",user.Mobile??""),
                new("Avatar",user.Avatar!),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
        }
        protected async Task RefreshUserClaimsAsync(User user, bool isPersistent)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,user.id.ToString()),
                new(ClaimTypes.Name,user.UserName),
                new("FullName",$"{user.FirstName} {user.LastName}"),
                new("Mobile",user.Mobile??""),
                new("Avatar",user.Avatar!),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = isPersistent
            };
            await HttpContext.SignInAsync(principal, properties);
        }
    }
}
