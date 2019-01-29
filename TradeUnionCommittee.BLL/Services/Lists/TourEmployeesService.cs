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
    public class TourEmployeesService : ITourEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public TourEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TourEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.EventEmployeesRepository
                .GetWithIncludeToList(x => x.IdEmployee == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Tour,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TourEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<TourEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.TourEmployees);
            var result = await _database.EventEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<TourEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(TourEmployeesDTO item)
        {
            await _database.EventEmployeesRepository.Create(_mapperService.Mapper.Map<EventEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(TourEmployeesDTO item)
        {
            await _database.EventEmployeesRepository.Update(_mapperService.Mapper.Map<EventEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.TourEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}