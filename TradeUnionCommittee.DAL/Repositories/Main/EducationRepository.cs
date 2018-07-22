using System;
using System.Linq;
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

        public override ActualResult<Education> Get(long id)
        {
            var result = new ActualResult<Education>();
            try
            {
                result.Result = _db.Education.FirstOrDefault(x => x.IdEmployee == id);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }
    }
}