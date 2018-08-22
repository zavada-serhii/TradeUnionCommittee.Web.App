using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Account
{
    public interface IAccountService : IService<AccountDTO>
    {
        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();

        Task<ActualResult<RolesDTO>> GetRoleId(long id);

        Task<ActualResult> CheckEmail(string email);

        Task<ActualResult<AccountDTO>> GetAsync(string hashId);

        Task<ActualResult> DeleteAsync(string hashId);
    }
}
