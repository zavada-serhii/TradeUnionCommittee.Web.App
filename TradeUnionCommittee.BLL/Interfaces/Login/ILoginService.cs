using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Login
{
    public interface ILoginService
    {
        Task<ActualResult<string>> Login(string email, string password);
        void Dispose();
    }
}
