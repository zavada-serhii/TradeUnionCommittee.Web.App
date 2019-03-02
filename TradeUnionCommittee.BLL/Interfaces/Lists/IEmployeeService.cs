using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IEmployeeService : IDisposable
    {
        Task<ActualResult> AddEmployeeAsync(CreateEmployeeDTO dto);
        Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId);
        Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckIdentificationСode(string identificationСode);
        Task<bool> CheckMechnikovCard(string mechnikovCard);
    }
}