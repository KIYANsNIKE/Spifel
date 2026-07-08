using FluentValidation;
using Spifel.Domain.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Shared.Validations
{
    public static class PasswordValidationExtensions
    {  
            public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder,string name = "رمز عبور")
            {
            return ruleBuilder
                .NotEmpty().WithMessage($"{name} نمی‌تواند خالی باشد").WithErrorCode(nameof(ErrorCode.Required))

                .MinimumLength(8).WithMessage($"{name} باید حداقل ۸ کاراکتر باشد").WithErrorCode(nameof(ErrorCode.PasswordTooShort))

                .Matches("[A-Z]").WithMessage($"{name} باید حداقل یک حرف بزرگ داشته باشد").WithErrorCode(nameof(ErrorCode.PasswordWeak))

                .Matches("[a-z]").WithMessage($"{name} باید حداقل یک حرف کوچک داشته باشد").WithErrorCode(nameof(ErrorCode.PasswordWeak))

                .Matches("[0-9]").WithMessage($"{name} باید حداقل یک عدد داشته باشد").WithErrorCode(nameof(ErrorCode.PasswordWeak))

                .Matches("[^a-zA-Z0-9]").WithMessage($"{name} باید حداقل یک کاراکتر خاص داشته باشد").WithErrorCode(nameof(ErrorCode.PasswordWeak));
            }  
    }
}
