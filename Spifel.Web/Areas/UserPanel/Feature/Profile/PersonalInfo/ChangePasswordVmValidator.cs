using FluentValidation;
using Spifel.Domain.Common.Result;

namespace Spifel.Web.Areas.UserPanel.Feature.Profile.PersonalInfo;

public class PersonalInfoVmValidator : AbstractValidator<PersonalInfoVM>
{
    public PersonalInfoVmValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        //RuleFor(l => l.Name).NotEmpty().WithMessage("نام خود را وارد کنید");

        //RuleFor(l => l.LastName).NotEmpty().WithMessage("نام خانوادگی خود را وارد کنید");

        RuleFor(l => l.Email)/*.NotEmpty().WithMessage("ایمیل را وارد کنید")*/
            .EmailAddress().WithMessage("فرمت ایمیل صحیح نیست");

        RuleFor(l => l.PhoneNumber)/*.NotEmpty().WithMessage("شماره تلفن خود را وارد کنید")*/
            .Matches("/(^(0?9)|(\\+?989))\\d{9}/g").WithMessage("فرمت شماره موبایل صحیح نیست");

        //RuleFor(l => l.).NotEmpty().WithMessage("نام خود را وارد کنید");

        //RuleFor(l => l.Name).NotEmpty().WithMessage("نام خود را وارد کنید");

    }
}
