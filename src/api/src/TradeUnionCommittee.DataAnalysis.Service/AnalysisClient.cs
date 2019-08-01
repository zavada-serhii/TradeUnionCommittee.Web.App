using RestSharp;
using System;

namespace TradeUnionCommittee.DataAnalysis.Service
{
    public class AnalysisClient : RestClient
    {
        public AnalysisClient(Uri baseUrl) : base (baseUrl) { }

        public AnalysisClient(string baseUrl) : base(baseUrl) { }
    }
}