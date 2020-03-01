using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.PDF.Service.Contracts
{
    public interface IReportGeneratorService
    {
        (string FileName, byte[] Data) Generate(ReportModel model);
    }
}