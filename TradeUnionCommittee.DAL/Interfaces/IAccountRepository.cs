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
        Task<ActualResult> UpdateUserRoleAsync(string idUser, string role);
        Task<ActualResult> DeleteUserAsync(string idUser);
        Task<ActualResult> CheckEmailAsync(string email);
        Task<ActualResult<IEnumerable<Role>>> GetAllRolesAsync();
        Task<ActualResult> CreateRoleAsync(string name);
        Task<ActualResult> DeleteRoleAsync(string id);
    }
}