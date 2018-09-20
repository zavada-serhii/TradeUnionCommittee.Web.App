using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.BL
{
    public interface ICheckerService
    {
        Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, Enums.Services service, bool checkTuple = true);
        Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, Enums.Services service, bool checkTuple = true);
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

        public async Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, Enums.Services service, bool checkTuple = true)
        {
            return await Task.Run(() =>
            {
                if (_hashIdUtilities.CheckDecrypt(hashId, service, out long id))
                {
                    if (checkTuple)
                    {
                        return CheckTupleInDb(id, service) ? new ActualResult() : new ActualResult(Errors.TupleDeleted);
                    }
                    return new ActualResult();
                }
                return new ActualResult(Errors.InvalidId);
            });
        }

        public async Task<ActualResult<long>> CheckDecryptAndTupleInDbWithId(string hashId, Enums.Services service, bool checkTuple = true)
        {
            return await Task.Run(() =>
            {
                if (_hashIdUtilities.CheckDecrypt(hashId, service, out long id))
                {
                    if (checkTuple)
                    {
                        return CheckTupleInDb(id, service) ? new ActualResult<long> { Result = id } : new ActualResult<long>(Errors.TupleDeleted);
                    }
                    return new ActualResult<long> {Result = id};
                }
                return new ActualResult<long>(Errors.InvalidId);
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        private bool CheckTupleInDb(long id, Enums.Services service)
        {
            switch (service)
            {
                case Enums.Services.Position:
                    return _database.PositionRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.SocialActivity:
                    return _database.SocialActivityRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Privileges:
                    return _database.PrivilegesRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Award:
                    return _database.AwardRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.MaterialAid:
                    return _database.MaterialAidRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Hobby:
                    return _database.HobbyRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Travel:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 1).Result.Any();

                case Enums.Services.Wellness:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 2).Result.Any();

                case Enums.Services.Tour:
                    return _database.EventRepository.Find(x => x.Id == id && x.TypeId == 3).Result.Any();

                case Enums.Services.Activities:
                    return _database.ActivitiesRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Cultural:
                    return _database.CulturalRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Subdivision:
                    return _database.SubdivisionsRepository.Find(x => x.Id == id).Result.Any();

                case Enums.Services.Dormitory:
                    return _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == 1).Result.Any();

                case Enums.Services.Departmental:
                    return _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == 2).Result.Any();

                case Enums.Services.Employee:
                    return _database.EmployeeRepository.Find(x => x.Id == id).Result.Any();

                default:
                    throw new ArgumentOutOfRangeException(nameof(service), service, null);
            }
        }
    }
}