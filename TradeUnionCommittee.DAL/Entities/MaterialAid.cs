using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class MaterialAid
    {
        public MaterialAid()
        {
            MaterialAidEmployees = new HashSet<MaterialAidEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<MaterialAidEmployees> MaterialAidEmployees { get; set; }
    }
}
