using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    public class FamilyService : IFamilyService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public FamilyService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<FamilyDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.FamilyRepository.Find(x => x.IdEmployee == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<FamilyDTO>>>(result);
        }

        public async Task<ActualResult<FamilyDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Family);
            var result = await _database.FamilyRepository.GetById(id);
            return _mapperService.Mapper.Map<ActualResult<FamilyDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(FamilyDTO item)
        {
            await _database.FamilyRepository.Create(_mapperService.Mapper.Map<DAL.Entities.Family>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(FamilyDTO item)
        {
            await _database.FamilyRepository.Update(_mapperService.Mapper.Map<DAL.Entities.Family>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.FamilyRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Family));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}