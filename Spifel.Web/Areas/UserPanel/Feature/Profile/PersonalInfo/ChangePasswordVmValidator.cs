using FluentValidation;
using Spifel.Domain.Common.Result;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo;

public class PersonalInfoVmValidator : AbstractValidator<PersonalInfoVM>
{
    public PersonalInfoVmValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(l => l.Email)
            .EmailAddress().WithMessage("فرمت ایمیل صحیح نیست");

        RuleFor(l => l.PhoneNumber)
            .Matches(@"^09\d{9}$").When(v => !string.IsNullOrWhiteSpace(v.PhoneNumber)).WithMessage("فرمت شماره موبایل صحیح نیست");

    }
}
