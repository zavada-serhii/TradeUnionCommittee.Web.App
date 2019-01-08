using System.Collections.Generic;

namespace TradeUnionCommittee.BLL.DTO
{
    public abstract class BaseDashboardResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public IEnumerable<string> Labels { get; set; }
    }

    public class DataSet
    {
        public string Label { get; set; }
        public IEnumerable<double> Data { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class PieResult : BaseDashboardResult<double> { }

    public class BarResult : BaseDashboardResult<double> { }

    public class AreaResult : BaseDashboardResult<double> { }

    public class RadarResult : BaseDashboardResult<DataSet> { }

    public class LineResult : BaseDashboardResult<DataSet> { }
}