using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.BL
{
    public interface ICheckerService
    {
        Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, EnumCryptoUtilities crypto, bool checkTuple = true);
        Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, EnumCryptoUtilities crypto, bool checkTuple = true);
    }

    internal class CheckerService : ICheckerService
    {
        private readonly IUnitOfWork _database;
        private readonly ICryptoUtilities _cryptoUtilities;

        public CheckerService(IUnitOfWork database, ICryptoUtilities cryptoUtilities)
        {
            _database = database;
            _cryptoUtilities = cryptoUtilities;
        }

        public async Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, EnumCryptoUtilities crypto, bool checkTuple = true)
        {
            return await Task.Run(() =>
            {
                if (_cryptoUtilities.CheckDecrypt(hashId, crypto, out long id))
                {
                    if (checkTuple)
                    {
                        return CheckTupleInDb(id, crypto) ? new ActualResult() : new ActualResult(Errors.TupleDeleted);
                    }
                    return new ActualResult();
                }
                return new ActualResult(Errors.InvalidId);
            });
        }

        public async Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, EnumCryptoUtilities crypto, bool checkTuple = true)
        {
            return await Task.Run(() =>
            {
                if (_cryptoUtilities.CheckDecrypt(hashId, crypto, out long id))
                {
                    if (checkTuple)
                    {
                        return CheckTupleInDb(id, crypto) ? new ActualResult<long> { Result = id } : new ActualResult<long>(Errors.TupleDeleted);
                    }
                    return new ActualResult<long> {Result = id};
                }
                return new ActualResult<long>(Errors.InvalidId);
            });
        }

        private bool CheckTupleInDb(long id, EnumCryptoUtilities crypto)
        {
            switch (crypto)
            {
                case EnumCryptoUtilities.Account:
                    return _database.UsersRepository.FindUsers(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Role:
                    return false;

                case EnumCryptoUtilities.Position:
                    return _database.PositionRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.SocialActivity:
                    return _database.SocialActivityRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Privileges:
                    return _database.PrivilegesRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Award:
                    return _database.AwardRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.MaterialAid:
                    return _database.MaterialAidRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Hobby:
                    return _database.HobbyRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Travel:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 1).Result.Any();

                case EnumCryptoUtilities.Wellness:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 2).Result.Any();

                case EnumCryptoUtilities.Tour:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 3).Result.Any();

                case EnumCryptoUtilities.Activities:
                    return _database.ActivitiesRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Cultural:
                    return _database.CulturalRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Subdivision:
                    return _database.SubdivisionsRepository.Find(x => x.Id == id).Result.Any();

                case EnumCryptoUtilities.Dormitory:
                    return _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == 1).Result.Any();

                case EnumCryptoUtilities.Departmental:
                    return _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == 2).Result.Any();

                default:
                    throw new ArgumentOutOfRangeException(nameof(crypto), crypto, null);
            }
        }
    }
}