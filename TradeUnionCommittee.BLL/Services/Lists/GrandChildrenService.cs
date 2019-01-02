using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class GrandChildrenService : IGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public GrandChildrenService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GrandChildrenDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.GrandChildrenRepository.Find(x => x.IdEmployee == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<GrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<GrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.GrandChildren);
            var result = await _database.GrandChildrenRepository.GetById(id);
            return _mapperService.Mapper.Map<ActualResult<GrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(GrandChildrenDTO item)
        {
            await _database.GrandChildrenRepository.Create(_mapperService.Mapper.Map<GrandChildren>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(GrandChildrenDTO item)
        {
            await _database.GrandChildrenRepository.Update(_mapperService.Mapper.Map<GrandChildren>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.GrandChildrenRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.GrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}