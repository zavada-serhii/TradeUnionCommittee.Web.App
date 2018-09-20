using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;
        private readonly ICheckerService _checkerService;

        public DormitoryService(IUnitOfWork database, IAutoMapperService mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<DormitoryDTO>>>(_database.AddressPublicHouseRepository.Find(x => x.Type == 1)));

        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Dormitory);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DormitoryDTO>>(_database.AddressPublicHouseRepository.Get(check.Result))
                : new ActualResult<DormitoryDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DormitoryDTO dto)
        {
            _database.AddressPublicHouseRepository.Create(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(DormitoryDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Dormitory);
            if (check.IsValid)
            {
                _database.AddressPublicHouseRepository.Update(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Dormitory, false);
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