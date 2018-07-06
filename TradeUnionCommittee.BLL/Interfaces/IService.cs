using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<ActualResult<IEnumerable<T>>> GetAllAsync();
        Task<ActualResult<T>> GetAsync(long id);
        Task<ActualResult> CreateAsync(T item);
        Task<ActualResult> UpdateAsync(T item);
        Task<ActualResult> DeleteAsync(long id);
        void Dispose();
    }
}
