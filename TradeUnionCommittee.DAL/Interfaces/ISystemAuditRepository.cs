using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface ISystemAuditRepository
    {
        Task AuditAsync(Journal journal);
        Task<IEnumerable<string>> GetExistingPartitionInDbAsync();
        Task<IEnumerable<Journal>> FilterAsync(string namePartition, string email, DateTime startDate, DateTime endDate);
    }
}