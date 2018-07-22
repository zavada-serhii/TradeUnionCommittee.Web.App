using System;
using System.Linq;
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

        public override ActualResult<Scientific> Get(long id)
        {
            var result = new ActualResult<Scientific>();
            try
            {
                result.Result = _db.Scientific.FirstOrDefault(x => x.IdEmployee == id);

                if (result.Result == null)
                {
                    result.IsValid = false;
                    result.ErrorsList.Add("Data has been deleted or changed!");
                }
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