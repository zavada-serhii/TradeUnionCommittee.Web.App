namespace TradeUnionCommittee.BLL
{
    public class ConnectionStrings
    {
        public bool DefaultConnectionUseSSL { get; set; }
        public string DefaultConnection { get; set; }
        public string DefaultConnectionSSL { get; set; }

        public bool IdentityConnectionUseSSL { get; set; }
        public string IdentityConnection { get; set; }
        public string IdentityConnectionSSL { get; set; }

        public bool AuditConnectionUseSSL { get; set; }
        public string AuditConnection { get; set; }
        public string AuditConnectionSSL { get; set; }

        public bool CloudStorageConnectionUseSSL { get; set; }
        public string CloudStorageConnection { get; set; }
        public string CloudStorageConnectionSSL { get; set; }
        public CloudStorageCredentials CloudStorageCredentials { get; set; }

        public string DataAnalysisConnection { get; set; }
    }

    public class CloudStorageCredentials
    {
        public bool UseSSL { get; set; }
        public string Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}