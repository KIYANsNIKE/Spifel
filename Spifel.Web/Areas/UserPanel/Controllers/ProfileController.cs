using Microsoft.AspNetCore.Mvc;
using Spifel.Application.Services.Interfaces;
using Spifel.Web.Areas.UserPanel.Feature.Profile.ChangePassword;
using Spifel.Web.Extensions;
using System.Security.Claims;

namespace Spifel.Web.Areas.UserPanel.Controllers
{
    public class ProfileController(IAccountService _accountService) : UserPanelBaseController
    {

        public IActionResult ChangePassword()
        {
            var x = TempData["PasswordChanged"];
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM VM)
        {
            var validationResult = await new ChangePasswordVmValidator().ValidateAsync(VM);
            if (!validationResult.IsValid)
            {
                ModelState.AddFluentValidationErrors(validationResult);
                return View(VM);
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _accountService.ChangePasswordAsync(VM.ToChangePasswordDto(userId));

            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return View(VM);
            }
            
            TempData["PasswordChanged"] = true;
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
