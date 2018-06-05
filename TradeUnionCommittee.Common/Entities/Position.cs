using System.Collections.Generic;

namespace TradeUnionCommittee.Common.Entities
{
    public class Position
    {
        public Position()
        {
            PositionEmployees = new HashSet<PositionEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<PositionEmployees> PositionEmployees { get; set; }
    }
}
