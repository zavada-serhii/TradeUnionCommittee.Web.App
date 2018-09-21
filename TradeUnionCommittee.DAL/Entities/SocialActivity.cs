using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class SocialActivity
    {
        public SocialActivity()
        {
            SocialActivityEmployees = new HashSet<SocialActivityEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<SocialActivityEmployees> SocialActivityEmployees { get; set; }
    }
}
