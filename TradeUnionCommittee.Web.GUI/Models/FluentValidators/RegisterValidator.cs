using FluentValidation;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;

namespace TradeUnionCommittee.Web.GUI.Models.FluentValidators
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email не може бути порожнім");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Некоректний Email");

            RuleFor(x => x.Password).NotNull().WithMessage("Пароль не може бути порожнім");
        }
    }
}