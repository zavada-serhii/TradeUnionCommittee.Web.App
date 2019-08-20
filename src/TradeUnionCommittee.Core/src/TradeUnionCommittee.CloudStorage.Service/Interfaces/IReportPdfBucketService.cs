using System.Threading.Tasks;
using TradeUnionCommittee.CloudStorage.Service.Model;

namespace TradeUnionCommittee.CloudStorage.Service.Interfaces
{
    public interface IReportPdfBucketService
    {
        Task<byte[]> GetObject(long id);
        Task PutPdfObject(ReportPdfBucketModel model);
    }
}