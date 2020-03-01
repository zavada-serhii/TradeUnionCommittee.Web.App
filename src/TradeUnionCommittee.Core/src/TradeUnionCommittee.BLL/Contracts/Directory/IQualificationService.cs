using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;

namespace TradeUnionCommittee.BLL.Contracts.Directory
{
    public interface IQualificationService : IDisposable
    {
        Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync();
        Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync();
    }
}