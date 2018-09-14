using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IRoleRepository
    {
        Task<ActualResult<IEnumerable<IdentityRole>>> GetAllRolesAsync();
        Task<ActualResult> CreateRoleAsync(string name);
        Task<ActualResult> DeleteRoleAsync(string id);
    }
}