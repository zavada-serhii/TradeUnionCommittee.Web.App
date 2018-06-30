using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _database;

        public LoginService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<LoginDTO>> Login(string email, string password)
        {
            return await Task.Run(() =>
            {
                var result = new ActualResult<LoginDTO> {Result = new LoginDTO()};

                var user =_database.UsersRepository.Find(x => x.Email == email && x.Password == password);

                if (user.IsValid && user.Result.Any())
                {
                    var role = _database.RolesRepository.GetAll();

                    var account = from r in role.Result
                        join u in user.Result
                        on r.Id equals u.IdRole
                        select new 
                        {
                            u.Email,
                            Role = r.Name
                        };

                    foreach (var accountsDto in account)
                    {
                        result.Result.Email = accountsDto.Email;
                        result.Result.Role = accountsDto.Role;
                    }
                }
                else
                {
                    result.IsValid = false;
                    result.ErrorsList.Add(new Error(DateTime.Now, "Не правильний логін або пароль!"));
                    return result;
                }
                return result;
            });
        }
    }
}
