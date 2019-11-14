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

    public class BarResult : BaseDashboardResult<int> { }

    public class AreaResult : BaseDashboardResult<double> { }

    public class RadarResult : BaseDashboardResult<DataSet> { }

    public class LineResult : BaseDashboardResult<DataSet> { }

    public class BubbleResult
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public IEnumerable<Bubble> Data { get; set; }
    }

    public class Bubble
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class BasicColumn
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<SeriesBasicColumn> Series { get; set; }
    }

    public class SeriesBasicColumn
    {
        public string Name { get; set; }
        public IEnumerable<double> Data { get; set; }
    }
}