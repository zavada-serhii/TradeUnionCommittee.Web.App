using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public DormitoryService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DormitoryDTO>>>(await _database.AddressPublicHouseRepository.Find(x => x.Type == TypeHouse.Dormitory));

        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Dormitory);
            var result = await _database.AddressPublicHouseRepository.Find(x => x.Id == id && x.Type == TypeHouse.Dormitory);
            return _mapperService.Mapper.Map<ActualResult<DormitoryDTO>>(new ActualResult<AddressPublicHouse>{ Result = result.Result.FirstOrDefault()});
        }

        public async Task<ActualResult> CreateAsync(DormitoryDTO dto)
        {
            if (!await CheckDuplicateDataAsync(dto))
            {
                await _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DormitoryDTO dto)
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
            await _database.AddressPublicHouseRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Dormitory));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckDuplicateDataAsync(DormitoryDTO dto)
        {
            var result = await _database.AddressPublicHouseRepository
                                        .Find(p => p.City == dto.City &&
                                                   p.Street == dto.Street &&
                                                   p.NumberHouse == dto.NumberHouse &&
                                                   p.NumberDormitory == dto.NumberDormitory &&
                                                   p.Type == TypeHouse.Dormitory);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}