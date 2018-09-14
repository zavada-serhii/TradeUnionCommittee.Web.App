using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult> Login(string email, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
                return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
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
            return await Task.Run(() =>
            {
                try
                {
                    return new ActualResult<IEnumerable<User>> { Result = _userManager.Users.ToList() };
                }
                catch (Exception e)
                {
                    return new ActualResult<IEnumerable<User>>(e.Message);
                }
            });
        }

        //-----------------------------------------------------------------------------------

        public async Task<ActualResult<User>> GetUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return user != null ? new ActualResult<User> { Result = user } : new ActualResult<User>(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult<User>(e.Message);
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
                        UserRoles = userRoles
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

        public async Task<ActualResult> UpdateUserRoleAsync(string idUser, List<string> roles)
        {
            try
            {
                // получаем пользователя
                var user = await _userManager.FindByIdAsync(idUser);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
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
    }
}