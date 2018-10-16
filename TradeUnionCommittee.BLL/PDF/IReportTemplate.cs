using TradeUnionCommittee.BLL.PDF.Models;

namespace TradeUnionCommittee.BLL.PDF
{
    public interface IReportTemplate
    {
        void CreateTemplateReport(ReportModel model);
    }
}