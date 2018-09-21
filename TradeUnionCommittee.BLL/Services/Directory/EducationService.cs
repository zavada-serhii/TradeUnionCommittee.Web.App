using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;

        public EducationService(IUnitOfWork database, IAutoMapperUtilities mapperService)
        {
            _database = database;
            _mapperService = mapperService;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync()
        {
            var result = await GetEducation();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.LevelEducation).Distinct() };
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync()
        {
            var result = await GetEducation();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.NameInstitution).Distinct() };
        }

        private async Task<ActualResult<IEnumerable<Education>>> GetEducation()
        {
            return await _database.EducationRepository.GetAll();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<EducationDTO>> GetEducationEmployeeAsync(long idEmployee)
        {
            var education = await _database.EducationRepository.Find(x => x.IdEmployee == idEmployee);
            return new ActualResult<EducationDTO> { Result = _mapperService.Mapper.Map<EducationDTO>(education.Result.FirstOrDefault()) };
        }

        public async Task<ActualResult> UpdateEducationEmployeeAsync(EducationDTO dto)
        {
            await _database.EducationRepository.Update(_mapperService.Mapper.Map<Education>(dto));
            return await _database.SaveAsync();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}