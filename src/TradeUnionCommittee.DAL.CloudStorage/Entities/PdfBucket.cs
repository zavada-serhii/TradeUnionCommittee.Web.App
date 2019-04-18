using System;
using System.Net;

namespace TradeUnionCommittee.DAL.CloudStorage.Entities
{
    public class PdfBucket
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public string EmailUser { get; set; }
        public ValueTuple<IPAddress, int> IpUser { get; set; }
        public int TypeReport { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
