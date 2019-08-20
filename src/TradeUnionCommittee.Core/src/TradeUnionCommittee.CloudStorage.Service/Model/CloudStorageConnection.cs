namespace TradeUnionCommittee.CloudStorage.Service.Model
{
    public class CloudStorageConnection
    {
        public bool UseSsl { get; set; }
        public string Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}