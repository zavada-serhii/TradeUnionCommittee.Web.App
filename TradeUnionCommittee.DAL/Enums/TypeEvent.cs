using NpgsqlTypes;

namespace TradeUnionCommittee.DAL.Enums
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
