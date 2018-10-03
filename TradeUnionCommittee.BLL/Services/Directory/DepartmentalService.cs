using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IHashIdUtilities _hashIdUtilities;

        public DepartmentalService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DepartmentalDTO>>>(await _database.AddressPublicHouseRepository.Find(x => x.Type == TypeHouse.Departmental));

        public async Task<ActualResult<Dictionary<string, string>>> GetAllShortcut()
        {
            var departmental = await _database.AddressPublicHouseRepository.Find(x => x.Type == TypeHouse.Departmental);
            var dictionary = departmental.Result.ToDictionary(result => _hashIdUtilities.EncryptLong(result.Id, Enums.Services.Departmental), result => result.City + " " + result.Street + " " + result.NumberHouse);
            return new ActualResult<Dictionary<string, string>> { Result = dictionary };
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Departmental);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DepartmentalDTO>>(await _database.AddressPublicHouseRepository.Get(check.Result))
                : new ActualResult<DepartmentalDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DepartmentalDTO dto)
        {
            await _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO dto)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(dto.HashId, Enums.Services.Departmental);
            if (check.IsValid)
            {
                await _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Departmental);
            if (check.IsValid)
            {
                await _database.AddressPublicHouseRepository.Delete(check.Result);
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