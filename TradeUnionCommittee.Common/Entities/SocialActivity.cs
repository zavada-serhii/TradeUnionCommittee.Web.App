using System.Collections.Generic;

namespace TradeUnionCommittee.Common.Entities
{
    public class SocialActivity
    {
        public SocialActivity()
        {
            SocialActivityEmployees = new HashSet<SocialActivityEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<SocialActivityEmployees> SocialActivityEmployees { get; set; }
    }
}
