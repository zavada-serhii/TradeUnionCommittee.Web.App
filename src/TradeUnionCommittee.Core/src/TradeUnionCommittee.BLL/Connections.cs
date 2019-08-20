namespace TradeUnionCommittee.BLL
{
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string IdentityConnection { get; set; }
        public string AuditConnection { get; set; }
        public string CloudStorageConnection { get; set; }
    }

    public class CloudStorageConnection
    {
        public bool UseSsl { get; set; }
        public string Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class RestConnection
    {
        public string DataAnalysisUrl { get; set; }
        public string ElasticUrl { get; set; }
    }
}