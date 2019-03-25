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
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public DormitoryService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAllAsync()
        {
            var dormitory = await _database.AddressPublicHouseRepository
                                           .FindWithOrderBy(x => x.Type == TypeHouse.Dormitory, c => c.NumberDormitory);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<DormitoryDTO>>>(dormitory);
        }


        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.AddressPublicHouse);
            var result = await _database.AddressPublicHouseRepository.GetByProperty(x => x.Id == id && x.Type == TypeHouse.Dormitory);
            return _mapperService.Mapper.Map<ActualResult<DormitoryDTO>>(result);
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
            await _database.AddressPublicHouseRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.AddressPublicHouse));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        private async Task<bool> CheckDuplicateDataAsync(DormitoryDTO dto)
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