using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Login;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _dataBase;

        public LoginService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }

        public async Task<ActualResult<string>> Login(string email, string password)
        {
            var role = await _dataBase.UsersRepository.Login(email, password);
            return role.IsValid ? new ActualResult<string> {Result = role.Result} : new ActualResult<string>(Errors.InvalidLoginOrPassword);
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}