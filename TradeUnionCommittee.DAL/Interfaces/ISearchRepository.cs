using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<long>> SearchByFullName(string fullName, AlgorithmSearchFullName algorithm);
    }
}