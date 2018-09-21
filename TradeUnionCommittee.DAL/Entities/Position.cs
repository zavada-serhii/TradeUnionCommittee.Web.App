using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Position
    {
        public Position()
        {
            PositionEmployees = new HashSet<PositionEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<PositionEmployees> PositionEmployees { get; set; }
    }
}
