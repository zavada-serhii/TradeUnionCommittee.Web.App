using System.Collections.Generic;

namespace TradeUnionCommittee.BLL.DTO
{
    public abstract class BaseDashboardResult
    {
        public IEnumerable<double> Data { get; set; }
        public IEnumerable<string> Labels { get; set; }
    }

    public class DataSet
    {
        public string Label { get; set; }
        public IEnumerable<double> Data { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class PieResult : BaseDashboardResult { }

    public class BarResult : BaseDashboardResult { }

    public class AreaResult : BaseDashboardResult { }

    public class RadarResult
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<DataSet> DataSets { get; set; }
    }

    public class LineResult
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<DataSet> DataSets { get; set; }
    }
}