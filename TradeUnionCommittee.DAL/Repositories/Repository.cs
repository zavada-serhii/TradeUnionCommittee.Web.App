using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
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

        public virtual async Task<ActualResult<IEnumerable<T>>> GetAll(Expression<Func<T, object>> orderBy = null)
        {
            try
            {
                if (orderBy == null)
                {
                    return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().ToListAsync() };
                }
                return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().OrderBy(orderBy).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public virtual async Task<ActualResult<T>> GetById(long id)
        {
            try
            {
                var result = await _db.Set<T>().FindAsync(id);
                return result != null ? new ActualResult<T> { Result = result } : new ActualResult<T>(Errors.TupleDeleted);
            }
            catch (Exception e)
            {
                return new ActualResult<T>(e.Message);
            }
        }

        public virtual async Task<ActualResult<T>> GetByProperty(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return new ActualResult<T> { Result = await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate) };
            }
            catch (Exception e)
            {
                return new ActualResult<T>(e.Message);
            }
        }

        public virtual async Task<ActualResult<IEnumerable<T>>> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().Where(predicate).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public async Task<ActualResult<IEnumerable<T>>> FindWithOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null)
        {
            try
            {
                if (orderBy == null)
                {
                    return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().Where(predicate).ToListAsync() };
                }
                return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().Where(predicate).OrderBy(orderBy).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public virtual async Task<ActualResult<IEnumerable<T>>> GetWithSelectorAndDistinct(Expression<Func<T, T>> selector, Expression<Func<T, object>> orderBy = null)
        {
            try
            {
                if (orderBy == null)
                {
                    return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().Select(selector).Distinct().ToListAsync() };
                }
                return new ActualResult<IEnumerable<T>> { Result = await _db.Set<T>().AsNoTracking().Select(selector).Distinct().OrderBy(orderBy).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public virtual async Task<ActualResult> Create(T item)
        {
            try
            {
                await _db.Set<T>().AddAsync(item);
                return new ActualResult();
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        public virtual async Task<ActualResult> Update(T item)
        {
            try
            {
                _db.Entry(item).State = EntityState.Modified;
                return await Task.Run(() => new ActualResult());
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        public virtual async Task<ActualResult> Delete(long id)
        {
            try
            {
                var result = await _db.Set<T>().FindAsync(id);
                if (result != null)
                {
                    _db.Set<T>().Remove(result);
                }
                return new ActualResult();
            }
            catch (Exception e)
            {
                return new ActualResult(e.Message);
            }
        }

        public virtual async Task<ActualResult<IEnumerable<T>>> GetWithIncludeToList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return new ActualResult<IEnumerable<T>> { Result = await Include(includeProperties).Where(predicate).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public virtual async Task<ActualResult<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return new ActualResult<T> { Result = await Include(includeProperties).FirstOrDefaultAsync(predicate) };
            }
            catch (Exception e)
            {
                return new ActualResult<T>(e.Message);
            }
        }

        public async Task<ActualResult<IEnumerable<T>>> GetWithIncludeAndOrderByToList(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                if (orderBy == null)
                {
                    return new ActualResult<IEnumerable<T>> { Result = await Include(includeProperties).Where(predicate).ToListAsync() };
                }
                return new ActualResult<IEnumerable<T>> { Result = await Include(includeProperties).Where(predicate).OrderBy(orderBy).ToListAsync() };
            }
            catch (Exception e)
            {
                return new ActualResult<IEnumerable<T>>(e.Message);
            }
        }

        public async Task<ActualResult<bool>> Any(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return new ActualResult<bool> { Result = await _db.Set<T>().AsNoTracking().AnyAsync(predicate) };
            }
            catch (Exception e)
            {
                return new ActualResult<bool>(e.Message);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(_db.Set<T>().AsNoTracking(), (current, includeProperty) => current.Include(includeProperty));
        }
    }
}