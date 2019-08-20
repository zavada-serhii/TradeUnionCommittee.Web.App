using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradeUnionCommittee.DAL.EF
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<TradeUnionCommitteeContext>
    {
        public TradeUnionCommitteeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeUnionCommitteeContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=TradeUnionCommitteeEmployeesCore;Port=5432;Username=postgres;Password=postgres;");
            return new TradeUnionCommitteeContext(optionsBuilder.Options);
        }
    }
}
