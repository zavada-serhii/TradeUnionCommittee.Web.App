using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class PositionEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdSubdivision { get; set; }
        public long IdPosition { get; set; }
        public bool CheckPosition { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public Position IdPositionNavigation { get; set; }
        public Subdivisions IdSubdivisionNavigation { get; set; }
    }
}
