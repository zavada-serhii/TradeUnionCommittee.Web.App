using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.ViewModels.Attributes
{
    public class RestructuringCompareAttribute : ValidationAttribute
    {
        public RestructuringCompareAttribute()
        {
            ErrorMessage = "Id не повинні співпадати!";
        }

        public override bool IsValid(object value)
        {
            return !(value is RestructuringViewModel model) || model.HashIdMain != model.HashIdSubordinate;
        }
    }
}
