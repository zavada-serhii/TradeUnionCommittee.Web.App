using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradeUnionCommittee.DAL.Identity.EF
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<TradeUnionCommitteeIdentityContext>
    {
        public TradeUnionCommitteeIdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeUnionCommitteeIdentityContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=TradeUnionCommitteeIdentity;Port=5432;Username=postgres;Password=postgres;");
            return new TradeUnionCommitteeIdentityContext(optionsBuilder.Options);
        }
    }
}