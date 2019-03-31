using NpgsqlTypes;

namespace TradeUnionCommittee.DAL.Enums
{
    public enum TypeHouse
    {
        [PgName("Dormitory")]
        Dormitory = 0,
        [PgName("Departmental")]
        Departmental = 1
    }
}
