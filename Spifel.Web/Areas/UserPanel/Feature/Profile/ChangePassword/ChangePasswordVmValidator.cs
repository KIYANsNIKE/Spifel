using FluentValidation;
using Spifel.Domain.Common.Result;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.ChangePassword
{
    public class ChangePasswordVmValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordVmValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(l => l.OldPassword).NotEmpty().WithMessage("رمز عبور فعلی خود را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور حداقل 8 کارکتر است");

            RuleFor(l => l.Password).NotEmpty().WithMessage("رمز عبور جدید را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور حداقل 8 کارکتر است");

            RuleFor(l => l.RePassword).NotEmpty().WithMessage("تکرار رمز عبور جدید را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور حداقل 8 کارکتر است")
                .Equal(l => l.Password).WithMessage("رمز عبور و تکرار آن مطابقت ندارند").WithErrorCode(nameof(ErrorCode.PasswordMismatch));

        }
    }
}
