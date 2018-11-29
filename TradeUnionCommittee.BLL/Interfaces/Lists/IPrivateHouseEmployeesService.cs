using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IPrivateHouseEmployeesService
    {
        Task<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee);
        Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId);
        void Dispose();
    }
}