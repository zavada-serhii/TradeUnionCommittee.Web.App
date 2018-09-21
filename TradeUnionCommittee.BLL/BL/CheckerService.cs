using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.BL
{
    public interface ICheckerService
    {
        Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, Enums.Services service);
        Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, Enums.Services service);
    }

    internal class CheckerService : ICheckerService
    {
        private readonly IUnitOfWork _database;
        private readonly IHashIdUtilities _hashIdUtilities;

        public CheckerService(IUnitOfWork database, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, Enums.Services service)
        {
            return await Task.Run(() =>
            {
                if (_hashIdUtilities.CheckDecrypt(hashId, service, out long _))
                {
                    return new ActualResult();
                }
                return new ActualResult(Errors.InvalidId);
            });
        }

        public async Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, Enums.Services service)
        {
            return await Task.Run(() =>
            {
                if (_hashIdUtilities.CheckDecrypt(hashId, service, out long id))
                {
                    return new ActualResult<long> {Result = id};
                }
                return new ActualResult<long>(Errors.InvalidId);
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}