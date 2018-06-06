namespace TradeUnionCommittee.DAL.Entities
{
    public class HobbyEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdHobby { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public Hobby IdHobbyNavigation { get; set; }
    }
}
