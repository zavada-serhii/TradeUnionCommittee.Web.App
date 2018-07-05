using System;
using System.Linq;
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

        public override ActualResult Create(Subdivisions item)
        {
            var result = new ActualResult();
            try
            {
                if (item.IdSubordinate == null)
                {
                    _dbContext.Subdivisions.Add(new Subdivisions
                    {
                        DeptName = item.DeptName
                    });
                }
                else
                {
                    _dbContext.Subdivisions.Add(new Subdivisions
                    {
                        DeptName = item.DeptName,
                        IdSubordinate = item.IdSubordinate
                    });
                }

            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public override ActualResult Update(Subdivisions item)
        {
            var result = new ActualResult();
            try
            {
                var subdivision =_dbContext.Subdivisions.FirstOrDefault(x => x.Id == item.Id);
                if (subdivision != null)
                {
                    if (item.IdSubordinate == null)
                    {
                        subdivision.DeptName = item.DeptName;
                    }
                    else
                    {
                        subdivision.IdSubordinate = item.IdSubordinate;
                    }
                }
                else
                {
                    result.IsValid = false;
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