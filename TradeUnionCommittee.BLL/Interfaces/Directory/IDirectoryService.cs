using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IDirectoryService
    {
        Task<ActualResult> CheckNameAsync(string name);
    }
}