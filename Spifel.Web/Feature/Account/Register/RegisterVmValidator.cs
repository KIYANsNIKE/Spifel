using FluentValidation;
using Spifel.Application.Shared.Validations;
using Spifel.Domain.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Web.Feature.Account.Register
{
    public class RegisterVmValidator : AbstractValidator<RegisterVM>
    {
        public RegisterVmValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("ایمیل را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                .EmailAddress().WithMessage("فرمت ایمیل صحیح نیست").WithErrorCode(nameof(ErrorCode.InvalidEmail))
                .MaximumLength(100).WithMessage("ایمیل نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.InvalidEmail));

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("نام کاربری را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                .MinimumLength(3).WithMessage("نام کاربری باید حداقل 3 کارکتر باشد").WithErrorCode(nameof(ErrorCode.PasswordTooShort))
                .MaximumLength(100).WithMessage("نام کاربری نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.UserNameToolong));

            RuleFor(v => v.Password)
                .StrongPassword();

            RuleFor(v => v.RePassword)
                .StrongPassword("تکرار رمز عبور")
                .Equal(v => v.Password).WithMessage("رمز عبور و تکرار آن مطابقت ندارند").WithErrorCode(nameof(ErrorCode.PasswordMismatch));
        }
    }
}
