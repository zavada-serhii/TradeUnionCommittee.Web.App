using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.SystemAudit
{
    public interface ISystemAuditService : IDisposable
    {
        Task AuditAsync(string email, string ipUser, Operations operation, Tables table);
        Task AuditAsync(string email, string ipUser, Operations operation, Tables[] table);
        Task<ActualResult<IEnumerable<JournalDTO>>> FilterAsync(string email, DateTime startDate, DateTime endDate);
    }
}
