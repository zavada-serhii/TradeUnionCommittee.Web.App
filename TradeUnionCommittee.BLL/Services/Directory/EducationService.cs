using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
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
            var result = await GetEducation(Education.LevelEducation);
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.LevelEducation).Distinct() };
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync()
        {
            var result = await GetEducation(Education.NameInstitution);
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.NameInstitution).Distinct() };
        }

        private async Task<ActualResult<IEnumerable<Employee>>> GetEducation(Education switcher)
        {
            switch (switcher)
            {
                case Education.LevelEducation:
                    return await _database.EmployeeRepository.GetWithSelectorAndDistinct(x => new Employee { LevelEducation = x.LevelEducation }, c => c.LevelEducation);
                case Education.NameInstitution:
                    return await _database.EmployeeRepository.GetWithSelectorAndDistinct(x => new Employee { NameInstitution = x.NameInstitution }, c => c.NameInstitution);
                default:
                    throw new ArgumentOutOfRangeException(nameof(switcher), switcher, null);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private enum Education
        {
            LevelEducation,
            NameInstitution
        }
    }
}