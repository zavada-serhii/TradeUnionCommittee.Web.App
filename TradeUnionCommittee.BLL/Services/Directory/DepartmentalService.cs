using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DepartmentalService : IDepartmentalService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;
        private readonly ICheckerService _checkerService;
        private readonly ICryptoUtilities _cryptoUtilities;

        public DepartmentalService(IUnitOfWork database, IAutoMapperService mapperService, ICheckerService checkerService, ICryptoUtilities cryptoUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
            _cryptoUtilities = cryptoUtilities;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<DepartmentalDTO>>>(_database.AddressPublicHouseRepository.Find(x => x.Type == 2)));

        public async Task<ActualResult<Dictionary<string, string>>> GetAllShortcut()
        {
            return await Task.Run(() =>
            {
                var departmental = _database.AddressPublicHouseRepository.Find(x => x.Type == 2);
                var dictionary = departmental.Result.ToDictionary(result => _cryptoUtilities.EncryptLong(result.Id, EnumCryptoUtilities.Departmental), result => result.City + " " + result.Street + " " + result.NumberHouse);
                return new ActualResult<Dictionary<string, string>> {Result = dictionary};
            });
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, BL.Services.Departmental);
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
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, BL.Services.Departmental);
            if (check.IsValid)
            {
                _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, BL.Services.Departmental, false);
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
            _checkerService.Dispose();
        }
    }
}