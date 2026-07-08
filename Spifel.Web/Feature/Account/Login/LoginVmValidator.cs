using FluentValidation;

namespace Spifel.Web.Feature.Account.Login
{
    public class LoginVmValidator : AbstractValidator<LoginVM>
    {
        public LoginVmValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(l => l.UserNameOrEmail).NotEmpty().WithMessage(" نام کاربری یا ایمیل را وارد کنید");

            RuleFor(l => l.Password).NotEmpty().WithMessage("رمز عبور را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور حداقل 8 کارکتر است");
        }

    }
}
