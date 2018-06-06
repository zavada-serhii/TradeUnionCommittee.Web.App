using System.Collections.Generic;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Privileges
    {
        public Privileges()
        {
            PrivilegeEmployees = new HashSet<PrivilegeEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<PrivilegeEmployees> PrivilegeEmployees { get; set; }
    }
}
