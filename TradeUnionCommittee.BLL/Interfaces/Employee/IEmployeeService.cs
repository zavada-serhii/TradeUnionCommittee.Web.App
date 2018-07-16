using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<ActualResult> AddEmployeeAsync(AddEmployeeDTO dto);

        Task<ActualResult> CheckIdentificationСode(string identificationСode);
        Task<ActualResult> CheckMechnikovCard(string mechnikovCard);

        void Dispose();
    }
}