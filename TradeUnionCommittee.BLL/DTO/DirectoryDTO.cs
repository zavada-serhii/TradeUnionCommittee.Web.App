namespace TradeUnionCommittee.BLL.DTO
{
    public class DirectoryDTO
    {
        public string HashId { get; set; }
        public string Name { get; set; }
        public uint RowVersion { get; set; }
    }

    public class SubdivisionDTO : DirectoryDTO
    {
        public string HashIdSubordinate { get; set; }
        public string Abbreviation { get; set; }
    }

    public class RolesDTO : DirectoryDTO
    {

    }

    public class TourDTO : DirectoryDTO
    {

    }

    public class TravelDTO : DirectoryDTO
    {

    }

    public class WellnessDTO : DirectoryDTO
    {

    }
}