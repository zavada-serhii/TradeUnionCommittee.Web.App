using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class , new()
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        protected Repository(TradeUnionCommitteeEmployeesCoreContext db)
        {
            _db = db;
        }

        public ActualResult<IEnumerable<T>> GetAll()
        {
            var result = new ActualResult<IEnumerable<T>>();
            try
            {
                result.Result = _db.Set<T>().ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult<T> Get(long id)
        {
            var result = new ActualResult<T>();
            try
            {
                result.Result = _db.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult Create(T item)
        {
            var result = new ActualResult();
            try
            {
                _db.Set<T>().Add(item);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult Edit(T item)
        {
            var result = new ActualResult();
            try
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public ActualResult Remove(long id)
        {
            var result = new ActualResult();
            try
            {
                var res = _db.Set<T>().Find(id);
                if (res != null)
                {
                    _db.Set<T>().Remove(res);
                    _db.SaveChanges();
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