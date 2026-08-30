using FluentValidation;
using Spifel.Domain.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations.Features.ChangeAvatar
{
    internal sealed class ChangeAvatarDtoValidator : AbstractValidator<ChangeAvatarDto>
    {
        public ChangeAvatarDtoValidator()
        {
            RuleFor(d => d.AvatarFile)
                .NotNull().WithMessage("لطفاً یک تصویر انتخاب کنید").WithErrorCode(nameof(ErrorCode.Required))
                .Must(file => file.ContentType.StartsWith("image/")).WithMessage("فایل انتخاب شده باید یک تصویر باشد").WithErrorCode(nameof(ErrorCode.InvalidFileType))
                .Must(file => file.Length <= 2 * 1024 * 1024).WithMessage("حجم تصویر نباید بیشتر از 2 مگابایت باشد").WithErrorCode(nameof(ErrorCode.FileTooLarge));
        }
    }
}
