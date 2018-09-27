using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface ISearchService
    {
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision);
        void Dispose();
    }
}