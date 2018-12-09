using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface ISearchService : IDisposable
    {
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchAccommodation(AccommodationType type, string dormitory, string departmental);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchBirthDate(CoverageType type, DateTime startDate, DateTime endDate);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchHobby(CoverageType type, string hobby);
        Task<ActualResult<string>> SearchEmployee(EmployeeType type, string value);
    }
}