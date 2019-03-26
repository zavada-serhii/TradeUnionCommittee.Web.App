using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAutoMapperConfiguration _mapperService;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IAutoMapperConfiguration mapperService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapperService = mapperService;
        }

        public async Task<ActualResult> Login(string email, string password, bool rememberMe, AuthorizationType type)
        {
            try
            {
                switch (type)
                {
                    case AuthorizationType.Cookie:
                        var resultCookie = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
                        return resultCookie.Succeeded ? new ActualResult() : new ActualResult(Errors.InvalidLoginOrPassword);
                    case AuthorizationType.Token:
                        var user = await _userManager.FindByNameAsync(email);
                        var resultToken = await _userManager.CheckPasswordAsync(user, password);
                        return resultToken ? new ActualResult() : new ActualResult(Errors.InvalidLoginOrPassword);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                if (users.Any())
                {
                    var result = new List<User>();
                    foreach (var user in users)
                    {
                        var userRole = await _userManager.GetRolesAsync(user);
                        var model = new User
                        {
                            Id = user.Id,
                            Email = user.Email,
                            UserRole = userRole.FirstOrDefault()
                        };
                        result.Add(model);
                    }
                    var mapping = _mapperService.Mapper.Map<IEnumerable<AccountDTO>>(result);
                    return new ActualResult<IEnumerable<AccountDTO>> { Result = mapping };
                }
                return new ActualResult<IEnumerable<AccountDTO>>(Errors.TupleDeleted);
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<AccountDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<AccountDTO>> GetUserAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    user.UserRole = userRole.FirstOrDefault();
                    var mapping = _mapperService.Mapper.Map<AccountDTO>(user);
                    return new ActualResult<AccountDTO> { Result = mapping };
                }
                return new ActualResult<AccountDTO>(Errors.TupleDeleted);
            }
            catch (Exception)
            {
                return new ActualResult<AccountDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<string>> GetRoleByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    return new ActualResult<string> { Result = userRole.FirstOrDefault() };
                }
                return new ActualResult<string>(Errors.TupleDeleted);
            }
            catch (Exception)
            {
                return new ActualResult<string>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateUserAsync(AccountDTO dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var user = new User
                    {
                        Email = dto.Email,
                        UserName = dto.Email,
                        Password = dto.Password,
                        UserRole = ConvertToEnglishLang(dto.Role)
                    };

                    var result = await _userManager.CreateAsync(user, user.Password);
                    if (result.Succeeded)
                    {
                        await UpdateUserRoleAsync(user.Id, user.UserRole);
                        return new ActualResult();
                    }
                    return new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserEmailAsync(AccountDTO dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var mapping = _mapperService.Mapper.Map<User>(dto);
                    var user = await _userManager.FindByIdAsync(mapping.Id);
                    if (user != null)
                    {
                        user.Email = mapping.Email;
                        user.UserName = mapping.Email;
                        var result = await _userManager.UpdateAsync(user);
                        return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserPasswordAsync(AccountDTO dto)
        {
            try
            {
                var mapping = _mapperService.Mapper.Map<User>(dto);
                var user = await _userManager.FindByIdAsync(mapping.Id);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, mapping.OldPassword, mapping.Password);
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateUserRoleAsync(AccountDTO dto)
        {
            return await UpdateUserRoleAsync(dto.HashIdUser, ConvertToEnglishLang(dto.Role));
        }

        private async Task<ActualResult> UpdateUserRoleAsync(string idUser, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(idUser);
                if (user != null)
                {
                    var roles = new List<string> { role };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var addedRoles = roles.Except(userRoles);
                    var removedRoles = userRoles.Except(roles);
                    await _userManager.AddToRolesAsync(user, addedRoles);
                    await _userManager.RemoveFromRolesAsync(user, removedRoles);
                    return new ActualResult();
                }
                return new ActualResult(Errors.TupleDeleted);
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
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.Select(identityRole => new Role { HashId = identityRole.Id, Name = identityRole.Name }).ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<RolesDTO>>(roles);
                return new ActualResult<IEnumerable<RolesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<RolesDTO>>(Errors.DataBaseError);
            }
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