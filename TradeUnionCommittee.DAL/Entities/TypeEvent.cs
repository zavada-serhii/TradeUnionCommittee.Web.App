using NpgsqlTypes;

namespace TradeUnionCommittee.DAL.Entities
{
    public enum TypeEvent
    {
        [PgName("Travel")]
        Travel = 0,
        [PgName("Wellness")]
        Wellness = 1,
        [PgName("Tour")]
        Tour = 2
    }
}
