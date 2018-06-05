using System.Collections.Generic;

namespace TradeUnionCommittee.Common.Entities
{
    public class Award
    {
        public Award()
        {
            AwardEmployees = new HashSet<AwardEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<AwardEmployees> AwardEmployees { get; set; }
    }
}
