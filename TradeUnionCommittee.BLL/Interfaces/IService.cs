using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<ActualResult<IEnumerable<T>>> GetAll();
        Task<ActualResult<T>> Get(long id);
        Task<ActualResult> Create(T item);
        Task<ActualResult> Update(T item);
        Task<ActualResult> Delete(long id);
        void Dispose();
    }
}
