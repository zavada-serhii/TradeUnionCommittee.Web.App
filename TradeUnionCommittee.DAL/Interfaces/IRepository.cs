using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        ActualResult<IEnumerable<T>> GetAll();
        ActualResult<T> Get(long id);
        ActualResult<IEnumerable<T>> Find(Func<T, bool> predicate);
        ActualResult Create(T item);
        ActualResult Update(T item);
        ActualResult Delete(long id);
        ActualResult<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        ActualResult<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}