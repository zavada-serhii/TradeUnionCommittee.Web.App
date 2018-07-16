using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _database;

        public EducationService(IUnitOfWork database)
        {
            _database = database;
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
    }
}