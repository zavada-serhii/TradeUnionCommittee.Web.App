using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Privileges
    {
        public Privileges()
        {
            PrivilegeEmployees = new HashSet<PrivilegeEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<PrivilegeEmployees> PrivilegeEmployees { get; set; }
    }
}
