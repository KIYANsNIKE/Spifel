using ASPSnippets.Core.Captcha;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using Spifel.Application.Services.Interfaces;
using Spifel.Application.Shared.Validations;
using Spifel.Domain.Common.Result;
using Spifel.Web.Extensions;
using Spifel.Web.Feature.Account.Login;
using Spifel.Web.Feature.Account.Register;
using System.Security.Claims;

namespace Spifel.Web.Controllers
{
    public class AccountController(IAccountService _accountService) : Controller
    {
        #region Captcha
        public Captcha Captcha
        {
            get
            {
                return JsonConvert.DeserializeObject<Captcha>(TempData["Captcha"].ToString());
            }
            set
            {
                TempData["Captcha"] = JsonConvert.SerializeObject(value);
            }
        }
        #endregion
        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            #region User side validation
            var validation = await new RegisterVmValidator().ValidateAsync(registerVM);
            if (!validation.IsValid)
            {
                ModelState.AddFluentValidationErrors(validation);
                return View(registerVM);
            }
            #endregion

            #region Server side validation
            var result = await _accountService.RegisterAsync(registerVM.ToRegisterDto());
            if (result.IsFailure)
            {
                ModelState.AddResultErrors(result.Errors);
                return View(registerVM);
            }
            #endregion
            return View("RegisterSuccess", registerVM);
        }

        #endregion
        #region Email Activation
        [Route("EmailVerification/{activeCode}")]
        public async Task<IActionResult> ActiveAcount(string activeCode)
        {
            ViewBag.IsAccoutActive = await _accountService.ActiveAccountAsync(activeCode);
            return View();
        }
        #endregion
        #region Login 
        [Route("Login")]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            this.Captcha = new Captcha(140);
            
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginVM() { ImageData = this.Captcha.ImageData});
        }

        // باید کپچا رو تمیز بنویسم 

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl)
        {
            var validationResult = await new LoginVmValidator(Captcha).ValidateAsync(loginVM);
            if (!validationResult.IsValid)
            {
                ModelState.AddFluentValidationErrors(validationResult);
                if (validationResult.Errors.Any(e=>e.PropertyName == "CaptchaAnswer"))
                {
                    this.Captcha = new Captcha(140);
                    loginVM.ImageData = this.Captcha.ImageData;
                }
                return View(loginVM);
            }

            var result = await _accountService.LoginUserAsync(loginVM.ToLoginDto());
            if (!result.IsSuccess)
            {
                if (result.Errors.Any(e => e.Code == ErrorCode.UserNotActive.ToString()))
                {
                    loginVM.IsUserNotActive = true;
                }
                //only normal errors not "UserNotActive"
                var errors = result.Errors.Where(e => e.Code != ErrorCode.UserNotActive.ToString()).ToList();
                ModelState.AddResultErrors(errors);

                //ADD Captcha
                this.Captcha = new Captcha(140);
                loginVM.ImageData = this.Captcha.ImageData;

                return View(loginVM);
            }

            var user = result.Value;
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,user.id.ToString()),
                new(ClaimTypes.Name,user.UserName),
                new("FullName",$"{user.FirstName} {user.LastName}")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = loginVM.RememberMe
            };

            await HttpContext.SignInAsync(principal, properties);
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect("/");
        }
        #endregion
        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion
    }
}
