using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists.Employee
{
    public interface IEmployeeService : IDisposable
    {
        Task<ActualResult> AddEmployeeAsync(CreateEmployeeDTO dto);
        Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId);
        Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckIdentificationCode(string identificationCode);
        Task<bool> CheckMechnikovCard(string mechnikovCard);
    }
}