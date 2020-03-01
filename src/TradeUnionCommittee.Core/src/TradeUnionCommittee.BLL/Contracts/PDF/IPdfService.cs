using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Contracts.PDF
{
    public interface IPdfService : IDisposable
    {
        Task<ActualResult<FileDTO>> CreateReport(ReportPdfDTO dto);
    }
}