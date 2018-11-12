using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IQualificationService
    {
        Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync();
        void Dispose();
    }
}