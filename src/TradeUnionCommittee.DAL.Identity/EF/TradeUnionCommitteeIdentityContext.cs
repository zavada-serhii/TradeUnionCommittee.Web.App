using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.DAL.Identity.Entities;

namespace TradeUnionCommittee.DAL.Identity.EF
{
    public sealed class TradeUnionCommitteeIdentityContext : IdentityDbContext<User>
    {
        public TradeUnionCommitteeIdentityContext(DbContextOptions<TradeUnionCommitteeIdentityContext> options) : base(options) { }
    }
}