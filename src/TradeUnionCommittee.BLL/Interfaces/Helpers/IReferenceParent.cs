using System;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Helpers
{
    public interface IReferenceParent : IDisposable
    {
        ActualResult<string> GetHashIdEmployee(string hashId, ReferenceParentType type);
    }
}