using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        protected Repository(TradeUnionCommitteeEmployeesCoreContext db)
        {
            _db = db;
        }

        public virtual ActualResult<IEnumerable<T>> GetAll()
        {
            var result = new ActualResult<IEnumerable<T>>();
            try
            {
                result.Result = _db.Set<T>().ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult<T> Get(long id)
        {
            var result = new ActualResult<T>();
            try
            {
                result.Result = _db.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult<IEnumerable<T>> Find(Func<T, bool> predicate)
        {
            var result = new ActualResult<IEnumerable<T>>();
            try
            {
                result.Result = _db.Set<T>().AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult Create(T item)
        {
            var result = new ActualResult();
            try
            {
                _db.Set<T>().Add(item);
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult Update(T item)
        {
            var result = new ActualResult();
            try
            {
                _db.Entry(item).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult Delete(long id)
        {
            var result = new ActualResult();
            try
            {
                var res = _db.Set<T>().Find(id);
                if (res != null)
                {
                    _db.Set<T>().Remove(res);
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            var result = new ActualResult<IEnumerable<T>>();
            try
            {
                result.Result = Include(includeProperties).ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public virtual ActualResult<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var result = new ActualResult<IEnumerable<T>>();
            try
            {
                var query = Include(includeProperties);
                result.Result = query.Where(predicate).ToList();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------

        // TODO: in the future use this implementation

        //public virtual async Task<ActualResult<IEnumerable<T>>> GetAll()
        //{
        //    try
        //    {
        //        return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().ToListAsync() };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult<IEnumerable<T>>(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult<T>> Get(long id)
        //{
        //    try
        //    {
        //        return new ActualResult<T> { Result = await _db.Set<T>().FindAsync(id) };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult<T>(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult<IEnumerable<T>>> Find(Func<T, bool> predicate)
        //{
        //    try
        //    {
        //        return await Task.Run(() => new ActualResult<IEnumerable<T>> { Result = _db.Set<T>().Where(predicate).ToList() });
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult<IEnumerable<T>>(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult> Create(T item)
        //{
        //    try
        //    {
        //        await _db.Set<T>().AddAsync(item);
        //        return new ActualResult();
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult> Update(T item)
        //{
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            _db.Entry(item).State = EntityState.Modified;
        //            return new ActualResult();
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult> Delete(long id)
        //{
        //    try
        //    {
        //        var result = await _db.Set<T>().FindAsync(id);
        //        if (result != null)
        //        {
        //            _db.Set<T>().Remove(result);
        //        }
        //        return new ActualResult();
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult<IEnumerable<T>>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        //{
        //    try
        //    {
        //        return new ActualResult<IEnumerable<T>> { Result = await Include(includeProperties).ToListAsync() };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult<IEnumerable<T>>(e.Message);
        //    }
        //}

        //public virtual async Task<ActualResult<IEnumerable<T>>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            var query = Include(includeProperties);
        //            return new ActualResult<IEnumerable<T>> { Result = query.Where(predicate).ToList() };
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return new ActualResult<IEnumerable<T>>(e.Message);
        //    }
        //}

        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _db.Set<T>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}