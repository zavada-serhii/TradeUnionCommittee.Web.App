using System.Collections.Generic;
using System.Linq;
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
    public class HobbyService : IHobbyService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;
        private readonly ICheckerService _checkerService;

        public HobbyService(IUnitOfWork database, IAutoMapperService mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(_database.HobbyRepository.GetAll()));

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Hobby);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<DirectoryDTO>>(_database.HobbyRepository.Get(check.Result))
                : new ActualResult<DirectoryDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                _database.HobbyRepository.Create(_mapperService.Mapper.Map<Hobby>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Hobby);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    _database.HobbyRepository.Update(_mapperService.Mapper.Map<Hobby>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Hobby, false);
            if (check.IsValid)
            {
                _database.HobbyRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckNameAsync(string name) =>
            await Task.Run(() => _database.HobbyRepository.Find(p => p.Name == name).Result.Any());

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}