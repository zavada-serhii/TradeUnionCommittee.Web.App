using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.DAL.Tests
{
    public class BaseClass
    {
        protected internal readonly UnitOfWork Work = new UnitOfWork("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=postgres;Password=postgres");
    }
}
