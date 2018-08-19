using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IScientificService
    {
        Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync();
        Task<ActualResult<ScientificDTO>> GetScientificEmployeeAsync(long idEmployee);
        Task<ActualResult> UpdateScientificEmployeeAsync(ScientificDTO dto);
    }
}