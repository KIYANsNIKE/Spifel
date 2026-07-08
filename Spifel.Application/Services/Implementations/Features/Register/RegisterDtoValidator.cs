using FluentValidation;
using Spifel.Application.Extentions;
using Spifel.Application.Shared.Validations;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Contracts;

namespace Spifel.Application.Services.Implementations.Features.Register
{
    internal sealed class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        private readonly IUserRepository _userRepository;
        public RegisterDtoValidator(IUserRepository userRepository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            _userRepository = userRepository;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("ایمیل را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                .EmailAddress().WithMessage("فرمت ایمیل صحیح نیست").WithErrorCode(nameof(ErrorCode.InvalidEmail))
                .MaximumLength(100).WithMessage("ایمیل نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.InvalidEmail))
                .MustAsync(BeUniqueEmail).WithMessage("این ایمیل قبلاً ثبت شده است").WithErrorCode(nameof(ErrorCode.EmailAlreadyExists));

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("نام کاربری را وارد کنید").WithErrorCode(nameof(ErrorCode.Required))
                .MinimumLength(3).WithMessage("نام کاربری باید حداقل 3 کارکتر باشد").WithErrorCode(nameof(ErrorCode.UserNameTooShort))
                .MaximumLength(100).WithMessage("نام کاربری نمیتواند بیشتر از 100 کارکتر باشد").WithErrorCode(nameof(ErrorCode.UserNameToolong))
                .MustAsync(BeUniqueUserName).WithMessage("این نام کاربری قبلاً ثبت شده است").WithErrorCode(nameof(ErrorCode.UserAlreadyExists));

            RuleFor(v => v.Password)
                .StrongPassword();

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
