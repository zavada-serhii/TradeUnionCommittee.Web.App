using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<ActualResult<IEnumerable<T>>> GetAll();
        Task<ActualResult<T>> Get(long? id);
        Task<ActualResult> Create(T item);
        Task<ActualResult> Edit(T item);
        Task<ActualResult> Remove(long? id);
        void Dispose();
    }
}
