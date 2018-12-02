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
    public class MaterialAidService : IMaterialAidService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public MaterialAidService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(await _database.MaterialAidRepository.GetAll());

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.MaterialAid);
            return _mapperService.Mapper.Map<ActualResult<DirectoryDTO>>(await _database.MaterialAidRepository.GetById(id));
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.MaterialAidRepository.Create(_mapperService.Mapper.Map<MaterialAid>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.MaterialAidRepository.Update(_mapperService.Mapper.Map<MaterialAid>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.MaterialAidRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.MaterialAid));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.MaterialAidRepository.Find(p => p.Name == name);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}