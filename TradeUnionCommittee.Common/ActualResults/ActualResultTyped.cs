using System.Collections.Generic;

namespace TradeUnionCommittee.Common.ActualResults
{
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
