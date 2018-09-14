using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //-----------------------------------------------------------------------------------
        public async Task<ActualResult<IEnumerable<IdentityRole>>> GetAllRolesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return new ActualResult<IEnumerable<IdentityRole>> { Result = _roleManager.Roles.ToList() };
                }
                catch (Exception e)
                {
                    return new ActualResult<IEnumerable<IdentityRole>>(e.Message);
                }
            });
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> CreateRoleAsync(string name)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return new ActualResult();
                }
                return result.Succeeded
                    ? new ActualResult()
                    : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteRoleAsync(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }
    }
}