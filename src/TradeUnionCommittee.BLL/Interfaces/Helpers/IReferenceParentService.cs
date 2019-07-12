using System;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Helpers
{
    public interface IReferenceParentService : IDisposable
    {
        ActualResult<string> GetHashIdEmployee(string hashId, ReferenceParentType type);
    }
}