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
    public class QualificationService : IQualificationService
    {
        private readonly IUnitOfWork _database;

        public QualificationService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            var result = await GetAllScientific(Qualification.ScientificDegree);
            return new ActualResult<IEnumerable<string>> {Result = result.Result.Select(x => x.ScientificDegree).Distinct().ToList()};
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            var result = await GetAllScientific(Qualification.ScientificTitle);
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.ScientificTitle).Distinct().ToList() };
        }

        private async Task<ActualResult<IEnumerable<Employee>>> GetAllScientific(Qualification switcher)
        {
            switch (switcher)
            {
                case Qualification.ScientificDegree:
                    return await _database.EmployeeRepository.GetWithSelectorAndDistinct(x => new Employee { ScientificDegree = x.ScientificDegree }, c => c.ScientificDegree);
                case Qualification.ScientificTitle:
                    return await _database.EmployeeRepository.GetWithSelectorAndDistinct(x => new Employee { ScientificTitle = x.ScientificTitle }, c => c.ScientificTitle);
                default:
                    throw new ArgumentOutOfRangeException(nameof(switcher), switcher, null);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public void Dispose()
        {
            _database.Dispose();
        }

        private enum Qualification
        {
            ScientificDegree,
            ScientificTitle
        }
    }
}