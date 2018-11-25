using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<ActualResult<IEnumerable<T>>> GetAll();
        Task<ActualResult<T>> Get(long id);
        Task<ActualResult<IEnumerable<T>>> Find(Expression<Func<T, bool>> predicate);
        Task<ActualResult> Create(T item);
        Task<ActualResult> Update(T item);
        Task<ActualResult> Delete(long id);
        Task<ActualResult<IEnumerable<T>>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        Task<ActualResult<IEnumerable<T>>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}