using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces.Account
{
    public interface IUserRepository
    {
        ActualResult UpdatePersonalData(long idUser, string email, long idRole);
        ActualResult UpdatePassword(long idUser, string password);
    }
}
