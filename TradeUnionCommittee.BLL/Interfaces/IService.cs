using System.Collections.Generic;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces
{
    public interface IService<T>
    {
        ActualResult<IEnumerable<T>> GetAll();
        ActualResult<T> Get(long? id);
        ActualResult Create(T item);
        ActualResult Edit(T item);
        ActualResult Remove(long? id);
        void Dispose();
    }
}
