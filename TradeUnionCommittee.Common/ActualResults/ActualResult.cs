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

    public class ActualResult<T> : ActualResult
    {
        public T Result { get; set; }

        public ActualResult()
        {
        }

        public ActualResult(Error error) : base(error)
        {

        }

        public ActualResult(List<Error> errors) : base(errors)
        {

        }
    }
}