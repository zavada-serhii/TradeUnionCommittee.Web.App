using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TradeUnionCommittee.ViewModels.Attributes
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return AllowableValues?.Contains(value?.ToString().ToUpper()) == true ? 
                ValidationResult.Success : 
                new ValidationResult(string.Empty);
        }
    }
}
