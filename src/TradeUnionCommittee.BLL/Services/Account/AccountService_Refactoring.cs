using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Account
{
    internal class AccountService_Refactoring
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AutoMapperConfiguration _mapperService;

        public AccountService_Refactoring(UserManager<User> userManager, 
                                          RoleManager<IdentityRole> roleManager, 
                                          SignInManager<User> signInManager, 
                                          AutoMapperConfiguration mapperService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapperService = mapperService;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The method for the authorization API, if the authorization was successful, returns the role otherwise null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async Task<string> Login(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var checkPassword = await _userManager.CheckPasswordAsync(user, password);
                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return roles.FirstOrDefault();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The method for the authorization MVC, if the authorization was successful, returns true else false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public async Task<bool> Login(string email, string password, bool rememberMe)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
                return result.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
    }
}