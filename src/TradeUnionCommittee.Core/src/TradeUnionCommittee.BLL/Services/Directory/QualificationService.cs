using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class QualificationService : IQualificationService
    {
        private readonly TradeUnionCommitteeContext _context;

        public QualificationService(TradeUnionCommitteeContext context)
        {
            _context = context;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            try
            {
                var result = await _context.Employee.Select(x => x.ScientificDegree).Distinct().OrderBy(x => x).ToListAsync();
                return new ActualResult<IEnumerable<string>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<string>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            try
            {
                var result = await _context.Employee.Select(x => x.ScientificTitle).Distinct().OrderBy(x => x).ToListAsync();
                return new ActualResult<IEnumerable<string>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<string>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync()
        {
            try
            {
                var result = await _context.Employee.Select(x => x.LevelEducation).Distinct().OrderBy(x => x).ToListAsync();
                return new ActualResult<IEnumerable<string>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<string>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync()
        {
            try
            {
                var result = await _context.Employee.Select(x => x.NameInstitution).Distinct().OrderBy(x => x).ToListAsync();
                return new ActualResult<IEnumerable<string>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<string>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}