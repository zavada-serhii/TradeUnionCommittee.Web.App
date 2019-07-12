using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Employee;

namespace TradeUnionCommittee.BLL.Interfaces.Lists.Employee
{
    public interface IEmployeeService : IDisposable
    {
        Task<ActualResult<string>> AddEmployeeAsync(CreateEmployeeDTO dto);
        Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId);
        Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<ActualResult<bool>> CheckIdentificationCode(string identificationCode);
        Task<ActualResult<bool>> CheckMechnikovCard(string mechnikovCard);
    }
}