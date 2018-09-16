using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;

        public EducationService(IUnitOfWork database, IAutoMapperService mapperService)
        {
            _database = database;
            _mapperService = mapperService;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.EducationRepository.GetAll().Result.Select(x => x.LevelEducation).Distinct().ToList()
                };
            });
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.EducationRepository.GetAll().Result.Select(x => x.NameInstitution).Distinct().ToList()
                };
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<EducationDTO>> GetEducationEmployeeAsync(long idEmployee)
        {
            return await Task.Run(() =>
            {
                var education = _database.EducationRepository.GetWithInclude(x => x.IdEmployee == idEmployee).Result.FirstOrDefault();
                return new ActualResult<EducationDTO> {Result = _mapperService.Mapper.Map<EducationDTO>(education) };
            });
        }

        public async Task<ActualResult> UpdateEducationEmployeeAsync(EducationDTO dto)
        {
            _database.EducationRepository.Update(_mapperService.Mapper.Map<Education>(dto));
            return await _database.SaveAsync();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}