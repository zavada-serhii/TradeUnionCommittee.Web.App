using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Account
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AutoMapperConfiguration _mapperService;

        public AccountService(UserManager<User> userManager, 
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

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var mapping = _mapperService.Mapper.Map<IEnumerable<AccountDTO>>(users);
                return new ActualResult<IEnumerable<AccountDTO>> { Result = mapping };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<AccountDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<AccountDTO>> GetAccountAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var mapping = _mapperService.Mapper.Map<AccountDTO>(user);
                    return new ActualResult<AccountDTO> { Result = mapping };
                }
                return new ActualResult<AccountDTO>(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult<AccountDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<AccountRoleDTO>> GetAccountRoleAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var result = new AccountRoleDTO { HashId = hashId, Role = ConvertToUkrainianLang(roles.FirstOrDefault()) };
                    return new ActualResult<AccountRoleDTO> { Result = result };
                }
                return new ActualResult<AccountRoleDTO>(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult<AccountRoleDTO>(Errors.DataBaseError);
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

        public async Task<ActualResult> CreateAsync(CreateAccountDTO dto)
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

        public async Task<ActualResult> UpdatePersonalDataAsync(AccountDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    user.Patronymic = dto.Patronymic;
                    var result = await _userManager.UpdateAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateEmailAsync(AccountDTO dto)
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

        public async Task<ActualResult> UpdatePasswordAsync(UpdateAccountPasswordDTO dto)
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

        public async Task<ActualResult> UpdateRoleAsync(AccountRoleDTO dto)
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
                var addToRole = await _userManager.AddToRolesAsync(user, addedRoles);
                var removeFromRole = await _userManager.RemoveFromRolesAsync(user, removedRoles);
                if (addToRole.Succeeded && removeFromRole.Succeeded)
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

        public async Task<ActualResult> DeleteAsync(string hashId)
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

        private string ConvertToUkrainianLang(string param)
        {
            switch (param)
            {
                case "Admin":
                    return "Адміністратор";
                case "Accountant":
                    return "Бухгалтер";
                case "Deputy":
                    return "Заступник";
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