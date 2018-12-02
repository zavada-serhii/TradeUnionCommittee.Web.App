using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.PDF
{
    public interface IPdfService
    {
        Task<ActualResult<byte[]>> CreateReport(ReportPdfDTO dto);
        void Dispose();
    }
}