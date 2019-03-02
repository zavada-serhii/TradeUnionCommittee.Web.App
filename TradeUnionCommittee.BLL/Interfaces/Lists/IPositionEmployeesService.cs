using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IPositionEmployeesService : IDisposable
    {
        Task<ActualResult<PositionEmployeesDTO>> GetAsync(string hashIdEmployee);
        Task<ActualResult> UpdateAsync(PositionEmployeesDTO dto);
    }
}