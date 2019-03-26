using Microsoft.EntityFrameworkCore;
using System.Linq;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.DAL.Extensions
{
    public static class ExtensionsDataBaseContext
    {
        public static void UndoChanges(this TradeUnionCommitteeContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
