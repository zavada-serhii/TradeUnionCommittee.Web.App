using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class SocialActivityEmployeesService : ISocialActivityEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public SocialActivityEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<SocialActivityEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.SocialActivityEmployeesRepository.GetWithInclude(x => x.IdEmployee == id, c => c.IdSocialActivityNavigation);
            return _mapperService.Mapper.Map<ActualResult<SocialActivityEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(SocialActivityEmployeesDTO dto)
        {
            dto.CheckSocialActivity = true;
            await _database.SocialActivityEmployeesRepository.Create(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(SocialActivityEmployeesDTO dto)
        {
            await _database.SocialActivityEmployeesRepository.Update(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.SocialActivityEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.SocialActivityEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}