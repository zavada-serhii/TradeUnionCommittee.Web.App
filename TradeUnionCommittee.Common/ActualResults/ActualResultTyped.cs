using System.Collections.Generic;
using TradeUnionCommittee.Common.Enums;

namespace TradeUnionCommittee.Common.ActualResults
{
    public class ActualResult<T> : ActualResult
    {
        public T Result { get; set; }

        public ActualResult()
        {
        }

        public ActualResult(string error) : base(error)
        {

        }

        public ActualResult(List<string> errors) : base(errors)
        {

        }

        public ActualResult(Errors error) : base(error)
        {

        }

        public ActualResult(IEnumerable<Errors> errors) : base(errors)
        {

        }
    }
}
