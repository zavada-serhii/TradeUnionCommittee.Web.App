using RestSharp;

namespace TradeUnionCommittee.DataAnalysis.Service
{
    public class DataAnalysisClient : RestClient
    {
        public DataAnalysisClient(string baseUrl) : base(baseUrl) { }
    }
}