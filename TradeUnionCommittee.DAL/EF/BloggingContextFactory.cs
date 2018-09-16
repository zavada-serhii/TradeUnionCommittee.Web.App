using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradeUnionCommittee.DAL.EF
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<TradeUnionCommitteeEmployeesCoreContext>
    {
        public TradeUnionCommitteeEmployeesCoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeUnionCommitteeEmployeesCoreContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=TradeUnionCommitteeEmployeesCore;Port=5432;Username=postgres;Password=postgres;");
            return new TradeUnionCommitteeEmployeesCoreContext(optionsBuilder.Options);
        }
    }
}
