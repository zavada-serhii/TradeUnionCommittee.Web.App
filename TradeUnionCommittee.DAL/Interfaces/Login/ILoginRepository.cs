using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces.Login
{
    public interface ILoginRepository
    {
        Task<ActualResult<string>> Login(string email, string password);
    }
}
