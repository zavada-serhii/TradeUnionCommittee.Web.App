using FluentValidation;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;

namespace TradeUnionCommittee.Web.GUI.Models.FluentValidators
{
    public class DirectoryValidator : AbstractValidator<DirectoryViewModel>
    {
        public DirectoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Назва не може бути порожньою!");
        }
    }
}
