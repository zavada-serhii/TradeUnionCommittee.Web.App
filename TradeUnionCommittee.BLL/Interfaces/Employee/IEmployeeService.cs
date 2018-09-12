using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<ActualResult> AddEmployeeAsync(CreateEmployeeDTO dto);
        Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(long id);
        Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckIdentificationСode(string identificationСode);
        Task<bool> CheckMechnikovCard(string mechnikovCard);
        void Dispose();
    }
}