using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Account
{
    public interface IAccountService : IDisposable
    {
        /// <summary>
        /// The method for the authorization API, if the authorization was successful, returns the role otherwise null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        Task<string> SignIn(string email, string password);
        /// <summary>
        /// The method for the authorization MVC, if the authorization was successful, returns true else false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        Task<bool> SignIn(string email, string password, bool rememberMe);
        Task SignOut();

        Task<ActualResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<ActualResult<AccountDTO>> GetAccountAsync(string hashId);
        Task<ActualResult<AccountDTO>> GetAccountRoleAsync(string hashId);
        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();

        Task<ActualResult> CreateAsync(CreateAccountDTO dto);
        Task<ActualResult> UpdatePersonalDataAsync(AccountDTO dto);
        Task<ActualResult> UpdateEmailAsync(AccountDTO dto);
        Task<ActualResult> UpdatePasswordAsync(UpdateAccountPasswordDTO dto);
        Task<ActualResult> UpdateRoleAsync(AccountDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);

        Task<bool> CheckEmailAsync(string email);
    }
}