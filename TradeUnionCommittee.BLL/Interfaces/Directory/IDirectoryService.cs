using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IDirectoryService
    {
        Task<bool> CheckNameAsync(string name);
    }
}