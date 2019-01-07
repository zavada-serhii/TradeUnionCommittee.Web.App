using System.Collections.Generic;

namespace TradeUnionCommittee.BLL.DTO
{
    public class PieResult
    {
        public IEnumerable<double> Data { get; set; }
        public IEnumerable<string> Labels { get; set; }
    }
}
