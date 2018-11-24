namespace TradeUnionCommittee.BLL.DTO
{
    public abstract class BaseDTO
    {
        public virtual string Name { get; set; }
        public virtual uint RowVersion { get; set; }
    }

    public class DirectoryDTO : BaseDTO
    {
        public string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
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