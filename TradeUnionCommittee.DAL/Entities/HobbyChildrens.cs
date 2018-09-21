using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class HobbyChildrens
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdChildren { get; set; }
        [ConcurrencyCheck]
        public long IdHobby { get; set; }

        public Children IdChildrenNavigation { get; set; }
        public Hobby IdHobbyNavigation { get; set; }
    }
}
