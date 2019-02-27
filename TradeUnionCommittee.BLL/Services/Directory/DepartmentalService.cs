using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DepartmentalService : IDepartmentalService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public DepartmentalService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
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
            var dictionary = departmental.Result.ToDictionary(result => _hashIdUtilities.EncryptLong(result.Id, Enums.Services.AddressPublicHouse), result => $"{result.City}, {result.Street}, {result.NumberHouse}");
            return new ActualResult<Dictionary<string, string>> { Result = dictionary };
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.AddressPublicHouse);
            var result = await _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == TypeHouse.Departmental);
            return _mapperService.Mapper.Map<ActualResult<DepartmentalDTO>>(new ActualResult<AddressPublicHouse> { Result = result.Result.FirstOrDefault() });
        }

        public async Task<ActualResult> CreateAsync(DepartmentalDTO dto)
        {
            if (!await CheckDuplicateDataAsync(dto))
            {
                await _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO dto)
        {
            if (!await CheckDuplicateDataAsync(dto))
            {
                await _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.AddressPublicHouseRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.AddressPublicHouse));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        private async Task<bool> CheckDuplicateDataAsync(DepartmentalDTO dto)
        {
            var result = await _database.AddressPublicHouseRepository
                                .Find(p => p.City == dto.City && 
                                           p.Street == dto.Street && 
                                           p.NumberHouse == dto.NumberHouse && 
                                           p.Type == TypeHouse.Departmental);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}