namespace TradeUnionCommittee.DataAnalysis.Service.Models
{
    public class DataAnalysisConnection
    {
        public string Url { get; set; }
        public bool UseBasicAuthentication { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IgnoreCertificateValidation { get; set; }
    }
}
