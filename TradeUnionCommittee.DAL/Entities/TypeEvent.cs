namespace TradeUnionCommittee.DAL.Entities
{
    public class TypeEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Event Event { get; set; }
    }
}
