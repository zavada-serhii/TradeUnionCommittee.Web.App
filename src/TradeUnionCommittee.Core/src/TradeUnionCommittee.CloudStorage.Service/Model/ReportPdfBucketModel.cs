using System;

namespace TradeUnionCommittee.CloudStorage.Service.Model
{
    public class ReportPdfBucketModel
    {
        public long IdEmployee { get; set; }
        public string FileName { get; set; }
        public string EmailUser { get; set; }
        public string IpUser { get; set; }
        public int TypeReport { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public byte[] Data { get; set; }
    }
}