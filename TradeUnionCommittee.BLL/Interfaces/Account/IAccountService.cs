using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Account
{
    public interface IAccountService : IService<AccountsDTO>
    {
        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();

        Task<ActualResult<RolesDTO>> GetRoleId(long id);

        Task<ActualResult<AccountsDTO>> Login(string login, string password);
    }
}
