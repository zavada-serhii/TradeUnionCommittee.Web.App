using FluentValidation;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.ViewModels.FluentValidation
{
    public class PositionEmployeesValidation : AbstractValidator<UpdatePositionEmployeesViewModel>
    {
        public PositionEmployeesValidation()
        {
            RuleFor(x => x.EndDate).Null().When(x => x.EndDate <= x.StartDate).WithMessage("Дата завершення повинна перевищувати дату початку");
        }
    }
}