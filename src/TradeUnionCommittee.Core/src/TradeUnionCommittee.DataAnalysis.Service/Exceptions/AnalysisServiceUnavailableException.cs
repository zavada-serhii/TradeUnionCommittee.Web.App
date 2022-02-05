namespace TradeUnionCommittee.DataAnalysis.Service.Exceptions
{
    public class AnalysisServiceUnavailableException : Exception 
    {
        public AnalysisServiceUnavailableException() { }

        public AnalysisServiceUnavailableException(string message): base(message) { }
    }
}