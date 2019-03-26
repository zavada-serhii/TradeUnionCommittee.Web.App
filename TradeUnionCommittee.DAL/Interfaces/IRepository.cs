using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<ActualResult<IEnumerable<T>>> FindWithOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null);
        Task<ActualResult<IEnumerable<T>>> GetWithIncludeAndOrderByToList(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] includeProperties);
        Task<ActualResult<bool>> Any(Expression<Func<T, bool>> predicate);



        Task<ActualResult<T>> GetById(long id);
        Task<ActualResult<T>> GetByProperty(Expression<Func<T, bool>> predicate);
        Task<ActualResult<IEnumerable<T>>> Find(Expression<Func<T, bool>> predicate);

        Task<ActualResult<IEnumerable<T>>> GetWithIncludeToList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<ActualResult<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<ActualResult> Create(T item);
        Task<ActualResult> Update(T item);
        Task<ActualResult> Delete(long id);
    }
}