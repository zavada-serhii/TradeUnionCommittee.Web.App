using System.Collections.Generic;

namespace TradeUnionCommittee.BLL.DTO
{
    public class ChartResult<T>
    {
        public T Chart { get; set; }
        public string DateTime { get; } = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public abstract class BaseChart<T>
    {
        public IEnumerable<T> Data { get; set; }
        public IEnumerable<string> Labels { get; set; }
    }

    public class DataSetChart<T>
    {
        public string Label { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class PieChart<T> : BaseChart<T> { }

    public class BarChart<T> : BaseChart<T> { }

    public class AreaChart<T> : BaseChart<T> { }

    public class RadarChart<T> : BaseChart<DataSetChart<T>> { }

    public class LineChart<T> : BaseChart<DataSetChart<T>> { }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class BubbleChart
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

    public class BasicColumnChart
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<Series> Series { get; set; }
    }

    public class Series
    {
        public string Name { get; set; }
        public IEnumerable<double> Data { get; set; }
    }
}