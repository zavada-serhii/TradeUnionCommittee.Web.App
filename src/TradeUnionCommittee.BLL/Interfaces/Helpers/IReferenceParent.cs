using System;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Helpers
{
    public interface IReferenceParent : IDisposable
    {
        ActualResult<string> GetHashIdEmployee(string hashId, ReferenceParentType type);
    }
}