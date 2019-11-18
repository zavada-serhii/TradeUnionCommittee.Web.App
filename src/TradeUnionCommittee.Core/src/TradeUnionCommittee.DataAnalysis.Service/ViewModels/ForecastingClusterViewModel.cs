using System.Collections.Generic;

namespace TradeUnionCommittee.DataAnalysis.Service.ViewModels
{
    public class ForecastingClusterViewModel
    {
        public IEnumerable<IEnumerable<double>> X { get; set; }
        public IEnumerable<IEnumerable<double>> Y { get; set; }
        public IEnumerable<IEnumerable<double>> Centers { get; set; }
    }
}