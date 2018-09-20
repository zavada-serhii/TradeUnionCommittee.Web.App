using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Activities
    {
        public Activities()
        {
            ActivityChildrens = new HashSet<ActivityChildrens>();
            ActivityEmployees = new HashSet<ActivityEmployees>();
            ActivityFamily = new HashSet<ActivityFamily>();
            ActivityGrandChildrens = new HashSet<ActivityGrandChildrens>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<ActivityChildrens> ActivityChildrens { get; set; }
        public ICollection<ActivityEmployees> ActivityEmployees { get; set; }
        public ICollection<ActivityFamily> ActivityFamily { get; set; }
        public ICollection<ActivityGrandChildrens> ActivityGrandChildrens { get; set; }
    }
}
