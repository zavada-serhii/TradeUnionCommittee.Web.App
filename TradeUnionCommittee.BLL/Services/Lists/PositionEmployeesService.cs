using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class PositionEmployeesService : IPositionEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public PositionEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<PositionEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            return _mapperService.Mapper.Map<ActualResult<PositionEmployeesDTO>>(await _database.PositionEmployeesRepository.GetByProperty(x => x.IdEmployee == id));
        }

        public Task<ActualResult> CreateAsync(PositionEmployeesDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(PositionEmployeesDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(string hashId)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}