namespace TradeUnionCommittee.DataAnalysis.Service.ViewModels
{
    public class ForecastingSignificanceViewModel
    {
        public string FirstCriterion { get; set; }
        public string SecondCriterion { get; set; }
        public double TCriteria { get; set; }
        public double TStatistics { get; set; }
        public bool Differs { get; set; }
    }
}