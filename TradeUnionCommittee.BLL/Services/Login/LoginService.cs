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
            var result = new ActualResult<string>();
            var role = await _dataBase.LoginRepository.Login(email, password);
            if (role.IsValid)
            {
                result.Result = role.Result;
            }
            else
            {
                result.IsValid = false;
                result.ErrorsList.Add("Не правильний логін або пароль!");
                return result;
            }
            return result;
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}