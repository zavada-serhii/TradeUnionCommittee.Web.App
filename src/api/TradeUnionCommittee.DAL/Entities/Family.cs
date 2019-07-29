using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Family
    {
        public Family()
        {
            ActivityFamily = new HashSet<ActivityFamily>();
            CulturalFamily = new HashSet<CulturalFamily>();
            EventFamily = new HashSet<EventFamily>();
        }

        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? BirthDate { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public ICollection<ActivityFamily> ActivityFamily { get; set; }
        public ICollection<CulturalFamily> CulturalFamily { get; set; }
        public ICollection<EventFamily> EventFamily { get; set; }
    }
}
