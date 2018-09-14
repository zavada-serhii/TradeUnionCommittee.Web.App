using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task<ActualResult> Login(string email, string password);
        Task<ActualResult> LogOff();
        Task<ActualResult<IEnumerable<User>>> GetAllUsersAsync();
        Task<ActualResult<User>> GetUserAsync(string id);
        Task<ActualResult<User>> GetUserWithRoleAsync(string id);
        Task<ActualResult> CreateUserAsync(User model);
        Task<ActualResult> UpdateUserEmailAsync(User model);
        Task<ActualResult> UpdateUserPasswordAsync(User model);
        Task<ActualResult> UpdateUserRoleAsync(string idUser, List<string> roles);
        Task<ActualResult> DeleteUserAsync(string idUser);
    }
}