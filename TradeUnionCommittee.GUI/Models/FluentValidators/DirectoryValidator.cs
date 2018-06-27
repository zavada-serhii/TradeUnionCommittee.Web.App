using FluentValidation;
using TradeUnionCommittee.GUI.Models.ViewModels;

namespace TradeUnionCommittee.GUI.Models.FluentValidators
{
    public class DirectoryValidator : AbstractValidator<DirectoryViewModel>
    {
        public DirectoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Назва не може бути порожньою!");
        }
    }
}
