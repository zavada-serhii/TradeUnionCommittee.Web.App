using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.EF;
using TradeUnionCommittee.Common.Entities;
using TradeUnionCommittee.DAL.Interfaces.Directory;

namespace TradeUnionCommittee.DAL.Repositories.Directory
{
    public class PositionRepository : IPositionRepository
    {
        public ActualResult<IEnumerable<Position>> GetAll()
        {
            var result = new ActualResult<IEnumerable<Position>>();

            try
            {
                using (var context = new TradeUnionCommitteeEmployeesCoreContext())
                {
                    result.Result = context.Position.ToList();
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult<Position> Get(int id)
        {
            var result = new ActualResult<Position>();

            try
            {
                using (var context = new TradeUnionCommitteeEmployeesCoreContext())
                {
                    result.Result = context.Position.Find(id);
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult Create(Position item)
        {
            var result = new ActualResult();
            try
            {
                using (var context = new TradeUnionCommitteeEmployeesCoreContext())
                {
                    context.Position.Add(item);
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult Edit(Position item)
        {
            var result = new ActualResult();
            try
            {
                using (var context = new TradeUnionCommitteeEmployeesCoreContext())
                {
                    context.Entry(item).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }
        
        public ActualResult Remove(int id)
        {
            var result = new ActualResult();
            try
            {
                using (var context = new TradeUnionCommitteeEmployeesCoreContext())
                {
                    var res = context.Position.Find(id);
                    if (res != null)
                        context.Position.Remove(res);
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }
    }
}