using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
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