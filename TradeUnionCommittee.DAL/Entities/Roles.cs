using System.Collections.Generic;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
