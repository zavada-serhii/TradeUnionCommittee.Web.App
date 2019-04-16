using System.Collections.Generic;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.ActualResults
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

        public ActualResult(IEnumerable<string> errors) : base(errors)
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
