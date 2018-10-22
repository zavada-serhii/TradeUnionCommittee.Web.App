using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Search
{
    public interface IReportService
    {
        Task CreateReport(PdfDTO dto, ReportType type);
        void Dispose();
    }
}