using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
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
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DormitoryDTO>>>(await _database.AddressPublicHouseRepository.Find(x => x.Type == 1));

        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Dormitory);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DormitoryDTO>>(await _database.AddressPublicHouseRepository.Get(check.Result))
                : new ActualResult<DormitoryDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DormitoryDTO dto)
        {
            await _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(DormitoryDTO dto)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(dto.HashId, Enums.Services.Dormitory);
            if (check.IsValid)
            {
                await _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Dormitory);
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