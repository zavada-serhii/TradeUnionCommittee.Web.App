using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountDTO_Refactoring
    {
        public string HashId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public interface IAccountService_Refactoring
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
        Task<ActualResult<IEnumerable<AccountDTO_Refactoring>>> GetAllUsersAsync();
        Task<ActualResult<AccountDTO_Refactoring>> GetUserAsync(string hashId);
        Task<ActualResult<AccountDTO_Refactoring>> GetUserWithRoleAsync(string hashId);
        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();
        Task<ActualResult> CreateUserAsync(AccountDTO_Refactoring dto);
        Task<ActualResult> UpdateUserPersonalDataAsync(AccountDTO_Refactoring dto);
        Task<ActualResult> UpdateUserPasswordAsync(AccountDTO_Refactoring dto);
        Task<ActualResult> UpdateUserRoleAsync(AccountDTO_Refactoring dto);
        Task<ActualResult> DeleteUserAsync(string hashId);
        Task<bool> CheckEmailAsync(string email);
    }

    internal class AccountService_Refactoring : IAccountService_Refactoring
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

        public async Task<string> SignIn(string email, string password)
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

        public async Task<bool> SignIn(string email, string password, bool rememberMe)
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

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<AccountDTO_Refactoring>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var mapping = _mapperService.Mapper.Map<IEnumerable<AccountDTO_Refactoring>>(users);
                return new ActualResult<IEnumerable<AccountDTO_Refactoring>> { Result = mapping };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<AccountDTO_Refactoring>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<AccountDTO_Refactoring>> GetUserAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var mapping = _mapperService.Mapper.Map<AccountDTO_Refactoring>(user);
                    return new ActualResult<AccountDTO_Refactoring> { Result = mapping };
                }
                return new ActualResult<AccountDTO_Refactoring>(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult<AccountDTO_Refactoring>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<AccountDTO_Refactoring>> GetUserWithRoleAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    user.UserRole = role.FirstOrDefault();
                    var mapping = _mapperService.Mapper.Map<AccountDTO_Refactoring>(user);
                    return new ActualResult<AccountDTO_Refactoring> { Result = mapping };
                }
                return new ActualResult<AccountDTO_Refactoring>(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult<AccountDTO_Refactoring>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<RolesDTO>>(roles);
                return new ActualResult<IEnumerable<RolesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<RolesDTO>>(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> CreateUserAsync(AccountDTO_Refactoring dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var user = new User
                    {
                        Email = dto.Email,
                        UserName = dto.Email,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Patronymic = dto.Patronymic
                    };
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        return await UpdateUserRoleAsync(user, ConvertToEnglishLang(dto.Role));
                    }
                    return new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.DuplicateEmail);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserPersonalDataAsync(AccountDTO_Refactoring dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var user = await _userManager.FindByIdAsync(dto.HashId);
                    if (user != null)
                    {
                        user.Email = dto.Email;
                        user.UserName = dto.Email;
                        user.FirstName = dto.FirstName;
                        user.LastName = dto.LastName;
                        user.Patronymic = dto.Patronymic;
                        var result = await _userManager.UpdateAsync(user);
                        return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                    }
                    return new ActualResult(Errors.UserNotFound);
                }
                return new ActualResult(Errors.DuplicateEmail);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserPasswordAsync(AccountDTO_Refactoring dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.Password);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserRoleAsync(AccountDTO_Refactoring dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    return await UpdateUserRoleAsync(user, ConvertToEnglishLang(dto.Role));
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        private async Task<ActualResult> UpdateUserRoleAsync(User user, string role)
        {
            try
            {
                var roles = new List<string> { role };
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                var tmp = await _userManager.AddToRolesAsync(user, addedRoles);
                var tmn = await _userManager.RemoveFromRolesAsync(user, removedRoles);
                if (tmp.Succeeded && tmn.Succeeded)
                {
                    return new ActualResult();
                }
                return new ActualResult(Errors.DataBaseError);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> DeleteUserAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private string ConvertToEnglishLang(string param)
        {
            switch (param)
            {
                case "Адміністратор":
                    return "Admin";
                case "Бухгалтер":
                    return "Accountant";
                case "Заступник":
                    return "Deputy";
                default:
                    return string.Empty;
            }
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
        }
    }
}