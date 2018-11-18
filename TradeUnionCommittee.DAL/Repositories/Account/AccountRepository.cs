using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> Login(string email, string password, AuthorizationType type)
        {
            try
            {
                switch (type)
                {
                    case AuthorizationType.Cookie:
                        var resultCookie = await _signInManager.PasswordSignInAsync(email, password, false, false);
                        return resultCookie.Succeeded ? new ActualResult() : new ActualResult(Errors.InvalidLoginOrPassword);
                    case AuthorizationType.Token:
                        var user = await _userManager.FindByNameAsync(email);
                        var resultToken = await _userManager.CheckPasswordAsync(user, password);
                        return resultToken ? new ActualResult() : new ActualResult(Errors.InvalidLoginOrPassword);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        public async Task<ActualResult> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new ActualResult();
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = _userManager.Users.ToList();
                if (users.Count > 0)
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
                    return new ActualResult<IEnumerable<User>> { Result = result };
                }
                return new ActualResult<IEnumerable<User>>(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<User>>(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult<User>> GetUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    user.UserRole = userRole.FirstOrDefault();
                    return new ActualResult<User> {Result = user};
                }
                return new ActualResult<User>(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult<User>(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

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
            catch (Exception e)
            {
                return new ActualResult<string>(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult<User>> GetUserWithRoleAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var model = new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserRole = userRoles.FirstOrDefault()
                    };
                    return new ActualResult<User> { Result = model };
                }
                return new ActualResult<User>(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult<User>(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> CreateUserAsync(User model)
        {
            try
            {
                var result = await _userManager.CreateAsync(model, model.Password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(model, false);
                    await UpdateUserRoleAsync(model.Id, model.UserRole);
                    return new ActualResult();
                }
                return new ActualResult(Errors.DataBaseError);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateUserEmailAsync(User model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    var result = await _userManager.UpdateAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateUserPasswordAsync(User model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateUserRoleAsync(string idUser, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(idUser);
                if (user != null)
                {
                    var roles = new List<string> {role};
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var addedRoles = roles.Except(userRoles);
                    var removedRoles = userRoles.Except(roles);
                    await _userManager.AddToRolesAsync(user, addedRoles);
                    await _userManager.RemoveFromRolesAsync(user, removedRoles);
                    return new ActualResult();
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteUserAsync(string idUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(idUser);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(result.Errors.Select(identityError => identityError.Description).ToList());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> CheckEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user == null ? new ActualResult(Errors.DuplicateData)  : new ActualResult();
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<Role>>> GetAllRolesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return new ActualResult<IEnumerable<Role>>
                    {
                        Result = _roleManager.Roles.ToList().Select(identityRole => new Role { HashId = identityRole.Id, Name = identityRole.Name }).ToList()
                    };
                }
                catch (Exception e)
                {
                    return new ActualResult<IEnumerable<Role>>(e.Message);
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