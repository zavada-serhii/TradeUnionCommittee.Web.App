using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class ScientificRepository : Repository<Scientific>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public ScientificRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }

        public override async  Task<ActualResult<Scientific>> Get(long id)
        {
            try
            {
                return new ActualResult<Scientific>
                {
                    Result = await _db.Scientific.AsNoTracking().FirstOrDefaultAsync(x => x.IdEmployee == id)
                };
            }
            catch (Exception e)
            {
                return new ActualResult<Scientific>(e.Message);
            }
        }
    }
}