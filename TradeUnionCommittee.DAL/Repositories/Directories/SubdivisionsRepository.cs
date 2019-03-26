using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class SubdivisionsRepository : Repository<Subdivisions>
    {
        private readonly TradeUnionCommitteeContext _dbContext;

        public SubdivisionsRepository(TradeUnionCommitteeContext db) : base(db)
        {
            _dbContext = db;
        }

        public override async Task<ActualResult> Update(Subdivisions item)
        {
            return await Task.Run(() =>
            {
                var result = new ActualResult();
                try
                {
                    switch (item.SubdivisionUpdate)
                    {
                        case Subdivision.UpdateName:
                            _dbContext.Entry(item).State = EntityState.Modified;
                            _dbContext.Entry(item).Property(x => x.IdSubordinate).IsModified = false;
                            _dbContext.Entry(item).Property(x => x.Abbreviation).IsModified = false;
                            break;
                        case Subdivision.UpdateAbbreviation:
                            _dbContext.Entry(item).State = EntityState.Modified;
                            _dbContext.Entry(item).Property(x => x.IdSubordinate).IsModified = false;
                            _dbContext.Entry(item).Property(x => x.Name).IsModified = false;
                            break;
                        case Subdivision.RestructuringUnits:
                            _dbContext.Entry(item).State = EntityState.Modified;
                            _dbContext.Entry(item).Property(x => x.Abbreviation).IsModified = false;
                            _dbContext.Entry(item).Property(x => x.Name).IsModified = false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception e)
                {
                    return new ActualResult(e.Message);
                }
                return result;
            });
        }
    }
}