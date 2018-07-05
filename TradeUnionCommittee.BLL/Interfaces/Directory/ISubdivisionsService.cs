using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface ISubdivisionsService : IService<SubdivisionDTO>, IDirectoryService
    {
        Task<ActualResult> RestructuringUnits(SubdivisionDTO dto);
        Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(long id);
    }
}