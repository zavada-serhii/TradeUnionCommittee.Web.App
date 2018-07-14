using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<ActualResult> AddEmployee(AddEmployeeDTO dto);
    }
}