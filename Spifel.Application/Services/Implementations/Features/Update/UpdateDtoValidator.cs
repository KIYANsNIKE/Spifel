using FluentValidation;
using Spifel.Application.Extentions;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Contracts;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations.Features.Update
{
    internal sealed class UpdateDtoValidator : AbstractValidator<UpdateDto>
    {
        private readonly IUserRepository _userRepository;
        public UpdateDtoValidator(IUserRepository userRepository, User user)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            _userRepository = userRepository;

            RuleFor(v => v.Email)
                 .NotEmpty().WithMessage("ایمیل را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                 .EmailAddress().WithMessage("فرمت ایمیل صحیح نیست").WithErrorCode(nameof(ErrorCode.InvalidEmail))
                 .MaximumLength(100).WithMessage("ایمیل نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.InvalidEmail))
                 .MustAsync(BeUniqueEmail).When(v => v.Email != user.Email).WithMessage("این ایمیل قبلاً ثبت شده است").WithErrorCode(nameof(ErrorCode.EmailAlreadyExists));

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("نام کاربری را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                .MinimumLength(3).WithMessage("نام کاربری باید حداقل 3 کارکتر باشد").WithErrorCode(nameof(ErrorCode.UserNameTooShort))
                .MaximumLength(100).WithMessage("نام کاربری نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.UserNameToolong))
                .MustAsync(BeUniqueUserName).When(v => v.UserName != user.UserName).WithMessage("این نام کاربری قبلاً ثبت شده است").WithErrorCode(nameof(ErrorCode.UserAlreadyExists));

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("نام نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.TooLong));

            RuleFor(v => v.LastName)
                .MaximumLength(100).WithMessage("نام خانوادگی نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.TooLong));

            RuleFor(v => v.PhoneNumber)
                .Matches(@"^09\d{9}$").When(v => !string.IsNullOrWhiteSpace(v.PhoneNumber)).WithMessage("فرمت شماره موبایل صحیح نیست").WithErrorCode(nameof(ErrorCode.InvalidPhoneNumber)); ;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken token)
        {
            return !await _userRepository.IsEmailExist(email.FixEmail());
        }

        private async Task<bool> BeUniqueUserName(string userName, CancellationToken token)
        {
            return !await _userRepository.IsUserNameExist(userName.FixUserName());
        }

    }
}
