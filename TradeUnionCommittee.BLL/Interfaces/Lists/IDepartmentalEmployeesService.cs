using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IDepartmentalEmployeesService
    {
        Task<ActualResult<IEnumerable<DepartmentalEmployeesDTO>>> GetAllAsync(string hashIdEmployee);
        Task<ActualResult<DepartmentalEmployeesDTO>> GetAsync(string hashId, string hashIdEmployee);
        Task<ActualResult> CreateAsync(DepartmentalEmployeesDTO item);
        Task<ActualResult> UpdateAsync(DepartmentalEmployeesDTO item);
    }
}
