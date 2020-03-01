using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Contracts.Lists.Employee
{
    public interface IPrivateHouseEmployeesService
    {
        Task<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PrivateHouse type);
        Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId);
        Task<ActualResult> CreateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type);
        Task<ActualResult> UpdateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type);
        Task<ActualResult> DeleteAsync(string hashId);
        void Dispose();
    }
}