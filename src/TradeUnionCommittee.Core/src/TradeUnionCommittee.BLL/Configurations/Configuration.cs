namespace TradeUnionCommittee.BLL.Configurations
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

    public class HashIdConfiguration
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public bool UseGuidFormat { get; set; }
    }
}