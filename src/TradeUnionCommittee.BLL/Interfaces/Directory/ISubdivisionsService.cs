using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface ISubdivisionsService : IDisposable, IDirectory<SubdivisionDTO>
    {
        Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId);
        Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId);
        Task<Dictionary<string, string>> GetSubordinateSubdivisionsForMvc(string hashId);
        Task<IEnumerable<TreeSubdivisionsDTO>> GetTreeSubdivisions();

        Task<ActualResult> CreateMainSubdivisionAsync(CreateSubdivisionDTO dto);
        Task<ActualResult> CreateSubordinateSubdivisionAsync(CreateSubordinateSubdivisionDTO dto);

        Task<ActualResult> UpdateNameSubdivisionAsync(UpdateSubdivisionNameDTO dto);
        Task<ActualResult> UpdateAbbreviationSubdivisionAsync(UpdateSubdivisionAbbreviationDTO dto);
        Task<ActualResult> RestructuringUnits(RestructuringSubdivisionDTO dto);

        Task<ActualResult> DeleteAsync(string hashId);
    }
}