using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface ISubdivisionsService //: IService<SubdivisionDTO>, IDirectoryService
    {
        Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync();
        Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId);
        Task<ActualResult> CreateMainSubdivisionAsync(SubdivisionDTO dto);
        Task<ActualResult> UpdateNameSubdivisionAsync(SubdivisionDTO dto);
        Task<ActualResult> UpdateAbbreviationSubdivisionAsync(SubdivisionDTO dto);
        Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId);
        Task<Dictionary<string, string>> GetSubordinateSubdivisionsForMvc(string hashId);
        Task<ActualResult> CreateSubordinateSubdivisionAsync(SubdivisionDTO dto);
        Task<ActualResult> RestructuringUnits(RestructuringSubdivisionDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckNameAsync(string name);
        Task<bool> CheckAbbreviationAsync(string name);
        void Dispose();
    }
}