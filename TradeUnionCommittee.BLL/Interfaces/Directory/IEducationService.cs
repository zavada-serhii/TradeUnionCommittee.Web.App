using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IEducationService
    {
        Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync();
        Task<ActualResult<EducationDTO>> GetEducationEmployeeAsync(long idEmployee);
        Task<ActualResult> UpdateEducationEmployeeAsync(EducationDTO dto);
    }
}