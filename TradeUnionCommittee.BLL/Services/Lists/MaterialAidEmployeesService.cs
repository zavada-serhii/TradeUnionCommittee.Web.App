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
    public class MaterialAidEmployeesService : IMaterialAidEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public MaterialAidEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<MaterialAidEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.MaterialAidEmployeesRepository.GetWithIncludeToList(x => x.IdEmployee == id, c => c.IdMaterialAidNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<MaterialAidEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<MaterialAidEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.MaterialAidEmployees);
            var result = await _database.MaterialAidEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdMaterialAidNavigation);
            return _mapperService.Mapper.Map<ActualResult<MaterialAidEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(MaterialAidEmployeesDTO item)
        {
            await _database.MaterialAidEmployeesRepository.Create(_mapperService.Mapper.Map<MaterialAidEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(MaterialAidEmployeesDTO item)
        {
            await _database.MaterialAidEmployeesRepository.Update(_mapperService.Mapper.Map<MaterialAidEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.MaterialAidEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.MaterialAidEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}