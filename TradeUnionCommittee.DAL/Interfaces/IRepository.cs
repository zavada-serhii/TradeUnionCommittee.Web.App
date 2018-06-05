using System.Collections.Generic;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        ActualResult<IEnumerable<T>> GetAll();
        ActualResult<T> Get(int id);
        ActualResult Create(T item);
        ActualResult Edit(T item);
        ActualResult Remove(int id);
    }
}
