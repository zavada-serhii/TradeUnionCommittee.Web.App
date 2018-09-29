using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface ISearchService
    {
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision);
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision);
        /// <summary>
        /// Input paratres for TypeAccommodation: dormitory or departmental or from-university.
        /// </summary>
        /// <param name="typeAccommodation"></param>
        /// <param name="dormitory"></param>
        /// <param name="departmental"></param>
        /// <returns></returns>
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchAccommodation(string typeAccommodation, string dormitory, string departmental);
        /// <summary>
        /// Input paratres for TypeBirthDate: employeeBirthDate or childrenBirthDate or grandChildrenBirthDate.
        /// </summary>
        /// <param name="typeBirthDate"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchBirthDate(string typeBirthDate, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Input paratres for TypeHobby: employeeHobby or childrenHobby or grandChildrenHobby.
        /// </summary>
        /// <param name="typeHobby"></param>
        /// <param name="hobby"></param>
        /// <returns></returns>
        Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchHobby(string typeHobby, string hobby);
        void Dispose();
    }
}