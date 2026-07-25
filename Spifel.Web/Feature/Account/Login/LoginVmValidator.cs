using ASPSnippets.Core.Captcha;
using FluentValidation;

namespace Spifel.Web.Feature.Account.Login
{
    public class LoginVmValidator : AbstractValidator<LoginVM>
    {
        public LoginVmValidator(Captcha captchaAnswer)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(l => l.UserNameOrEmail).NotEmpty().WithMessage(" نام کاربری یا ایمیل را وارد کنید");

            RuleFor(l => l.Password).NotEmpty().WithMessage("رمز عبور را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور حداقل 8 کارکتر است");

            RuleFor(l => l.CaptchaAnswer).NotEmpty().WithMessage("متن درون تصویر امنیتی را وارد کنید")
                .Must(a => captchaAnswer.IsValid(a)).WithMessage("عبارت امنیتی به درستی وارد نشده");
                
        }

    }
}
