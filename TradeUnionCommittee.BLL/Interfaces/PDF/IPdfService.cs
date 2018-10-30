using System.Threading.Tasks;
using TradeUnionCommittee.BLL.PDF.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.PDF
{
    public interface IPdfService
    {
        Task CreateReport(ReportPdfDTO dto);
        void Dispose();
    }
}