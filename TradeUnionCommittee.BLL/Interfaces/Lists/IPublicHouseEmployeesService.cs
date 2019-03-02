using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IPublicHouseEmployeesService : IService<PublicHouseEmployeesDTO>
    {
        Task<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PublicHouse type);
    }
}