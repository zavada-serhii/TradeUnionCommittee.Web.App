using System.Collections.Generic;

namespace TradeUnionCommittee.Common.ActualResults
{
    public class ActualResult
    {
        public List<Error> ErrorsList { get; set; }
        public bool IsValid { get; set; }

        public ActualResult()
        {
            IsValid = true;
            ErrorsList = new List<Error>();
        }

        public ActualResult(Error error)
        {
            IsValid = false;
            ErrorsList = new List<Error> { error };
        }

        public ActualResult(List<Error> errors)
        {
            IsValid = false;
            ErrorsList = errors;
        }
    }
}
