using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<long>> SearchByFullName(string fullName);
    }
}