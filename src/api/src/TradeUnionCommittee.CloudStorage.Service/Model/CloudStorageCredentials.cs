namespace TradeUnionCommittee.CloudStorage.Service.Model
{
    public class CloudStorageCredentials
    {
        public string DbConnectionString { get; set; }
        public bool UseStorageSsl { get; set; }
        public string Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}