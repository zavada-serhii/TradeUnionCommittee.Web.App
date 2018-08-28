using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public UsersRepository(TradeUnionCommitteeEmployeesCoreContext db)
        {
            _db = db;
        }

        public async Task<ActualResult<string>> Login(string email, string password)
        {
            var result = new ActualResult<string>();
            try
            {
                var user = await _db.Users
                    .Include(u => u.IdRoleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user != null && HashingPassword.VerifyHashedPassword(user.Password, password))
                {
                    result.Result = user.IdRoleNavigation.Name;
                }
                else
                {
                    result.IsValid = false;
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult<IEnumerable<Users>> GetAllUsers()
        {
            var result = new ActualResult<IEnumerable<Users>>();
            try
            {
               result.Result = _db.Users.Include(x => x.IdRoleNavigation).ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult<Users> GetUser(long id)
        {
            var result = new ActualResult<Users>();
            try
            {
                result.Result = _db.Users.Find(id);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult<IEnumerable<Users>> FindUsers(Func<Users, bool> predicate)
        {
            var result = new ActualResult<IEnumerable<Users>>();
            try
            {
                result.Result = _db.Users.AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult CreateUser(Users item)
        {
            var result = new ActualResult();
            try
            {
                item.Password = HashingPassword.HashPassword(item.Password);
                _db.Users.Add(item);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult UpdateUserEmail(Users item)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Find(item.Id);
                if (user != null)
                {
                    user.Email = item.Email;
                    _db.Entry(user).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult UpdateUserPassword(Users item)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Find(item.Id);
                if (user != null)
                {
                    user.Password = HashingPassword.HashPassword(item.Password);
                    _db.Entry(user).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult UpdateUserRole(Users item)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Find(item.Id);
                if (user != null)
                {
                    user.IdRole = item.IdRole;
                    _db.Entry(user).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult DeleteUser(long id)
        {
            var result = new ActualResult();
            try
            {
                var res = _db.Users.Find(id);
                if (res != null)
                {
                    _db.Users.Remove(res);
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public ActualResult<IEnumerable<Roles>> GetAllRoles()
        {
            var result = new ActualResult<IEnumerable<Roles>>();
            try
            {
                result.Result = _db.Roles.ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult<Roles> GetRole(long id)
        {
            var result = new ActualResult<Roles>();
            try
            {
                result.Result = _db.Roles.Find(id);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }
    }
}