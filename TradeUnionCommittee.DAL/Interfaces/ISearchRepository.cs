using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Repositories.Search;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<ResultFullNameSearch>> SearchByFullName(string fullName, TrigramSearch type);
    }
}