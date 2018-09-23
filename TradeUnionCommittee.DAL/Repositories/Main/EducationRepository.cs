using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class EducationRepository : Repository<Education>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public EducationRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<ActualResult<Education>> Get(long id)
        {
            var result = new ActualResult<Education>();
            try
            {
                result.Result = await _db.Education.FirstOrDefaultAsync(x => x.IdEmployee == id);
            }
            catch (Exception e)
            {
                return new ActualResult<Education>(e.Message);
            }
            return result;
        }
    }
}