using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradeUnionCommittee.DAL.EF
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<TradeUnionCommitteeEmployeesCoreContext>
    {
        public TradeUnionCommitteeEmployeesCoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeUnionCommitteeEmployeesCoreContext>();
            optionsBuilder.UseNpgsql("Host=anton-db-server.postgres.database.azure.com;Database=TradeUnionCommitteeEmployeesCore;Port=5432;Username=postgres@anton-db-server;Password=7355608@123dart-veyder;UseSslStream=true;SslMode=Require;");
            return new TradeUnionCommitteeEmployeesCoreContext(optionsBuilder.Options);
        }
    }
}
