using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Interfaces.Login;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.DAL.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public LoginRepository(TradeUnionCommitteeEmployeesCoreContext db)
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

                if (user != null && HashingPassword.VerifyHashedPassword(user.Password,password))
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
    }
}