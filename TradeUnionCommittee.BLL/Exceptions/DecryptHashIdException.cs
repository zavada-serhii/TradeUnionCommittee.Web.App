using System;

namespace TradeUnionCommittee.BLL.Exceptions
{
    public class DecryptHashIdException : Exception
    {
        public DecryptHashIdException(string message) : base(message) { }
    }
}