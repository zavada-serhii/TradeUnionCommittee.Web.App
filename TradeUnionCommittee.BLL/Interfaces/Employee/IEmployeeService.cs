using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<ActualResult> AddEmployeeAsync(AddEmployeeDTO dto);
        Task<ActualResult<MainInfoEmployeeDTO>> GetMainInfoEmployeeAsync(long id);
        Task<ActualResult> UpdateMainInfoEmployeeAsync(MainInfoEmployeeDTO dto);
        Task<ActualResult> DeleteAsync(long id);
        Task<ActualResult> CheckIdentificationСode(string identificationСode);
        Task<ActualResult> CheckMechnikovCard(string mechnikovCard);
        void Dispose();
    }
}