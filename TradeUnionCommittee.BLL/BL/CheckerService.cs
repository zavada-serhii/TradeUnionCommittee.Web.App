using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;

namespace TradeUnionCommittee.BLL.BL
{
    public interface ICheckerService
    {
        Task<ActualResult> CheckDecrypt(string hashId, Enums.Services service);
        Task<ActualResult<long>> CheckDecryptWithId(string hashId, Enums.Services service);
    }

    internal class CheckerService : ICheckerService
    {
        private readonly IHashIdUtilities _hashIdUtilities;

        public CheckerService(IHashIdUtilities hashIdUtilities)
        {
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult> CheckDecrypt(string hashId, Enums.Services service)
        {
            return await Task.Run(() => _hashIdUtilities.CheckDecrypt(hashId, service, out long _) ? new ActualResult() : new ActualResult(Errors.InvalidId));
        }

        public async Task<ActualResult<long>> CheckDecryptWithId(string hashId, Enums.Services service)
        {
            return await Task.Run(() => _hashIdUtilities.CheckDecrypt(hashId, service, out long id) ? new ActualResult<long> {Result = id} : new ActualResult<long>(Errors.InvalidId));
        }
    }
}