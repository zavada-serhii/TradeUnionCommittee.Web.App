using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.PDF.Service.Interfaces
{
    public interface IReportGeneratorService
    {
        byte[] Generate(ReportModel model);
    }
}