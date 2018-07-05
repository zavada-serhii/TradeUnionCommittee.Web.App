using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces.User
{
    public interface IUserRepository
    {
        ActualResult UpdatePersonalData(long idUser, string email, long idRole);
        ActualResult UpdatePassword(long idUser, string password);
    }
}
