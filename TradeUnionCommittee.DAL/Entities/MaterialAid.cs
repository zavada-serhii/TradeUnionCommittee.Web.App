using System.Collections.Generic;

namespace TradeUnionCommittee.DAL.Entities
{
    public class MaterialAid
    {
        public MaterialAid()
        {
            MaterialAidEmployees = new HashSet<MaterialAidEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<MaterialAidEmployees> MaterialAidEmployees { get; set; }
    }
}
