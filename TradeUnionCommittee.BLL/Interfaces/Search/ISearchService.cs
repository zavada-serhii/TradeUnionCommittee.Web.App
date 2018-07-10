using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface ISearchService
    {
        Task<IEnumerable<ResultSearchDTO>> ListAddedEmployeesTemp();
    }
}
