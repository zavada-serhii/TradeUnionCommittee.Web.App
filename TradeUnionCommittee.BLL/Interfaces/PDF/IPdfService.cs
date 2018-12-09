using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.PDF
{
    public interface IPdfService : IDisposable
    {
        Task<ActualResult<byte[]>> CreateReport(ReportPdfDTO dto);
    }
}