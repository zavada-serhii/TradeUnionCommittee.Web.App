using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class HobbyGrandChildrenService : IHobbyGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public HobbyGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.HobbyGrandChildrensRepository.GetWithIncludeToList(x => x.IdGrandChildren == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<HobbyGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<HobbyGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyGrandChildren);
            var result = await _database.HobbyGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<HobbyGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(HobbyGrandChildrenDTO item)
        {
            await _database.HobbyGrandChildrensRepository.Create(_mapperService.Mapper.Map<HobbyGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(HobbyGrandChildrenDTO item)
        {
            await _database.HobbyGrandChildrensRepository.Update(_mapperService.Mapper.Map<HobbyGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.HobbyGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<string> GetHashIdEmployee(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.GrandChildrenRepository.GetById(id);
            if (result.IsValid && result.Result != null)
            {
                return _hashIdUtilities.EncryptLong(result.Result.IdEmployee, Enums.Services.Employee);
            }
            return null;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}