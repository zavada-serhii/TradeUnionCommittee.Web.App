using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IQualificationService
    {
        Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync();
        Task<ActualResult<QualificationDTO>> GetQualificationEmployeeAsync(long idEmployee);
        Task<ActualResult> UpdateQualificationEmployeeAsync(QualificationDTO dto);
        Task<ActualResult> CreateQualificationEmployeeAsync(QualificationDTO dto);
        Task<ActualResult> DeleteQualificationEmployeeAsync(string hashId);
        void Dispose();
    }
}