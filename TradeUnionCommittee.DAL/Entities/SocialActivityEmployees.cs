using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class SocialActivityEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public long IdSocialActivity { get; set; }
        [ConcurrencyCheck]
        public string Note { get; set; }
        [ConcurrencyCheck]
        public bool CheckSocialActivity { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public SocialActivity IdSocialActivityNavigation { get; set; }
    }
}
