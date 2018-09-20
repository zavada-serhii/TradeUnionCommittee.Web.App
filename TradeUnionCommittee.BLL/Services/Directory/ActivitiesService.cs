using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly ICheckerService _checkerService;

        public ActivitiesService(IUnitOfWork database, IAutoMapperUtilities mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(_database.ActivitiesRepository.GetAll()));

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Activities);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DirectoryDTO>>(_database.ActivitiesRepository.Get(check.Result))
                : new ActualResult<DirectoryDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                _database.ActivitiesRepository.Create(_mapperService.Mapper.Map<Activities>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Activities);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    _database.ActivitiesRepository.Update(_mapperService.Mapper.Map<Activities>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Activities, false);
            if (check.IsValid)
            {
                _database.ActivitiesRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckNameAsync(string name) =>
            await Task.Run(() => _database.ActivitiesRepository.Find(p => p.Name == name).Result.Any());

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}