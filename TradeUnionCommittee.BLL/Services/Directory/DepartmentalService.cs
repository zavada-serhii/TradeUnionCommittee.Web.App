using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DepartmentalService : IDepartmentalService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly ICheckerService _checkerService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public DepartmentalService(IUnitOfWork database, IAutoMapperUtilities mapperService, ICheckerService checkerService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<DepartmentalDTO>>>(_database.AddressPublicHouseRepository.Find(x => x.Type == 2)));

        public async Task<ActualResult<Dictionary<string, string>>> GetAllShortcut()
        {
            return await Task.Run(() =>
            {
                var departmental = _database.AddressPublicHouseRepository.Find(x => x.Type == 2);
                var dictionary = departmental.Result.ToDictionary(result => _hashIdUtilities.EncryptLong(result.Id, Enums.Services.Departmental), result => result.City + " " + result.Street + " " + result.NumberHouse);
                return new ActualResult<Dictionary<string, string>> {Result = dictionary};
            });
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Departmental);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DepartmentalDTO>>(_database.AddressPublicHouseRepository.Get(check.Result))
                : new ActualResult<DepartmentalDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DepartmentalDTO dto)
        {
            _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Departmental);
            if (check.IsValid)
            {
                _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Departmental, false);
            if (check.IsValid)
            {
                _database.AddressPublicHouseRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}