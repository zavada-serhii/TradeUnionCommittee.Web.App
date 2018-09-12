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
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckIdentificationСode(string identificationСode);
        Task<bool> CheckMechnikovCard(string mechnikovCard);
        void Dispose();
    }
}