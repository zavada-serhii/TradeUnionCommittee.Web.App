using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces
{
    public interface IService<T> : IDisposable where T : class, new()
    {
        Task<ActualResult<T>> GetAsync(string hashId);
        Task<ActualResult> CreateAsync(T item);
        Task<ActualResult> UpdateAsync(T item);
        Task<ActualResult> DeleteAsync(string hashId);
    }

    public interface IDirectory<T> where T : class, new()
    {
        Task<ActualResult<IEnumerable<T>>> GetAllAsync();
    }

    public interface IList<T> where T : class, new()
    {
        Task<ActualResult<IEnumerable<T>>> GetAllAsync(string hashIdEmployee);
    }
}