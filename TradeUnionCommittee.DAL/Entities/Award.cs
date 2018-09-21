using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Award
    {
        public Award()
        {
            AwardEmployees = new HashSet<AwardEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<AwardEmployees> AwardEmployees { get; set; }
    }
}
