using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spifel.Application.Services.Interfaces;
using Spifel.Domain.Models.User;
using Spifel.Web.Areas.UserPanel.Feature.Profile.ChangeAvatar;
using Spifel.Web.Areas.UserPanel.Feature.Profile.ChangePassword;
using Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo;
using Spifel.Web.Extensions;
using System.Security.Claims;

namespace Spifel.Web.Areas.UserPanel.Controllers
{
    public class ProfileController(IAccountService _accountService) : UserPanelBaseController
    {
        [AllowAnonymous]
        [Route("ForceChange")]
        public async Task<IActionResult> ForcePasswordChange()
        {
            string password = "Asdcxz8905@";
            string username = "GTA_VI";
            var result = await _accountService.ForcePasswordChange(username, password);

            if (result.IsSuccess)
            {
                return Redirect("/Login");
            }

            return BadRequest();
        }
        //
        public async Task<IActionResult> PersonalInfo()
        {
            var user = await _accountService.GetUserByIdAsync(User.GetId());
            return View(user.Value.ToPersonalInfoVM());
        }

        #region Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM VM)
        {
            var validationResult = await new ChangePasswordVmValidator().ValidateAsync(VM);
            if (!validationResult.IsValid)
            {
                ModelState.AddFluentValidationErrors(validationResult);
                return BadRequest(new
                {
                    success = false,
                    errors = validationResult.Errors.Select(error => new
                    {
                        propertyName = error.PropertyName,
                        message = error.ErrorMessage
                    })
                });
            }

            var result = await _accountService.ChangePasswordAsync(VM.ToChangePasswordDto(User.GetId()));

            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return BadRequest(new
                {
                    success = false,
                    errors = result.Errors.Select(error => new
                    {
                        propertyName = error.PropertyName,
                        message = error.Description
                    })
                });
            }

            //TempData["Alert"] = "PasswordChanged";
            return Ok(new
            {
                success = true
            });
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(PersonalInfoVM VM)
        {
            // Validation
            var validationResult = await new PersonalInfoVmValidator().ValidateAsync(VM);
            if (!validationResult.IsValid)
            {
                ModelState.AddFluentValidationErrors(validationResult);
                return View(nameof(PersonalInfo), VM);
            }

            var result = await _accountService.UpdateUserAsync(VM.ToUpdateDto(User.GetId()));
            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return View(nameof(PersonalInfo), VM);
            }
            // Save

            return RedirectToAction(nameof(PersonalInfo));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAvatar(PersonalInfoVM VM)
        {
            // Validation 

            // Operation
            var result = await _accountService.ChangeAvatarAsync(VM.ToChangeAvatarDto(User.GetId()));

            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return View(nameof(PersonalInfo), VM);

            }
            else
            {
                TempData["Alert"] = "AvatarChanged";
                return RedirectToAction(nameof(PersonalInfo));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAvatar()
        {
            //Validation

            //oparation
            var result = await _accountService.DeleteAvatarAsync(User.GetId());

            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return View(nameof(PersonalInfo));
            }
            TempData["Alert"] = "AvatarDeleted";
            return RedirectToAction(nameof(PersonalInfo));
        }
    }
}
