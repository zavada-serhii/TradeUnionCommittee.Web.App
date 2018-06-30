using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Account
{
    public interface ILoginService
    {
        Task<ActualResult<LoginDTO>> Login(string email, string password);
    }
}
