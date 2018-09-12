using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Account
{
    public interface IAccountService
    {
        Task<ActualResult<string>> Login(string email, string password);

        Task<ActualResult<IEnumerable<AccountDTO>>> GetAllUsersAsync();
        Task<ActualResult<AccountDTO>> GetUserAsync(string hashId);
        Task<ActualResult> CreateUserAsync(AccountDTO dto);
        Task<ActualResult> UpdateUserEmailAsync(AccountDTO dto);
        Task<ActualResult> UpdateUserPasswordAsync(AccountDTO dto);
        Task<ActualResult> UpdateUserRoleAsync(AccountDTO dto);
        Task<ActualResult> DeleteUserAsync(string hashId);
        Task<bool> CheckEmailAsync(string email);

        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();
        Task<ActualResult<RolesDTO>> GetRoleId(string hashId);

        void Dispose();
    }
}