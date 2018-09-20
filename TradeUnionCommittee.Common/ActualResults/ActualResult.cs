using System.Collections.Generic;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.Common.Helpers;

namespace TradeUnionCommittee.Common.ActualResults
{
    public class ActualResult
    {
        public List<string> ErrorsList { get; set; }
        public bool IsValid { get; set; }

        public ActualResult()
        {
            IsValid = true;
            ErrorsList = new List<string>();
        }

        public ActualResult(string error)
        {
            IsValid = false;
            ErrorsList = new List<string> { error };
        }

        public ActualResult(List<string> errors)
        {
            IsValid = false;
            ErrorsList = errors;
        }

        public ActualResult(Errors error)
        {
            IsValid = false;
            ErrorsList = new List<string> { DescriptionErrorsHelper.DescriptionError(error) };
        }

        public ActualResult(IEnumerable<Errors> errors)
        {
            IsValid = false;
            ErrorsList = DescriptionErrorsHelper.DescriptionErrors(errors);
        }
    }
}