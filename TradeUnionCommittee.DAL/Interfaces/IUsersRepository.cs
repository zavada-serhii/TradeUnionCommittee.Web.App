using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<ActualResult<string>> Login(string email, string password);

        ActualResult<IEnumerable<Users>> GetAllUsers();
        ActualResult<Users> GetUser(long id);
        ActualResult<IEnumerable<Users>> FindUsers(Func<Users, bool> predicate);
        ActualResult CreateUser(Users item);
        ActualResult UpdateUserEmail(Users item);
        ActualResult UpdateUserPassword(Users item);
        ActualResult UpdateUserRole(Users item);
        ActualResult DeleteUser(long id);

        ActualResult<IEnumerable<Roles>> GetAllRoles();
        ActualResult<Roles> GetRole(long id);
    }
}
