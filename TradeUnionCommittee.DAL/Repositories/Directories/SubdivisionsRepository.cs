using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class SubdivisionsRepository : Repository<Subdivisions>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _dbContext;

        public SubdivisionsRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _dbContext = db;
        }

        public override async Task<ActualResult> Create(Subdivisions item)
        {
            var result = new ActualResult();
            try
            {
                if (item.IdSubordinate == null || item.IdSubordinate == 0)
                {
                    await _dbContext.Subdivisions.AddAsync(new Subdivisions
                    {
                        Name = item.Name,
                        Abbreviation = item.Abbreviation
                    });
                }
                else
                {
                    await _dbContext.Subdivisions.AddAsync(new Subdivisions
                    {
                        Name = item.Name,
                        Abbreviation = item.Abbreviation,
                        IdSubordinate = item.IdSubordinate
                    });
                }

            }
            catch (Exception e)
            {
               return new ActualResult(e.Message);
            }
            return result;
        }
    }
}