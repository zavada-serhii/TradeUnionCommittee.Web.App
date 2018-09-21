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
            var result = new ActualResult<Scientific>();
            try
            {
                result.Result = await _db.Scientific.FirstOrDefaultAsync(x => x.IdEmployee == id);

                if (result.Result == null)
                {
                    return new ActualResult<Scientific>("Data has been deleted or changed!");
                }
            }
            catch (Exception e)
            {
                return new ActualResult<Scientific>(e.Message);
            }
            return result;
        }
    }
}