using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface ISearchService
    {
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> ListAddedEmployeesTemp();
        Task SearchFullName(string fullName);
    }
}