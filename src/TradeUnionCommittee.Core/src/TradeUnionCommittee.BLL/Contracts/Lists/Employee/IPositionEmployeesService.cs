using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Employee;

namespace TradeUnionCommittee.BLL.Contracts.Lists.Employee
{
    public interface IPositionEmployeesService : IDisposable
    {
        Task<ActualResult<PositionEmployeesDTO>> GetAsync(string hashIdEmployee);
        Task<ActualResult> UpdateAsync(PositionEmployeesDTO dto);
    }
}