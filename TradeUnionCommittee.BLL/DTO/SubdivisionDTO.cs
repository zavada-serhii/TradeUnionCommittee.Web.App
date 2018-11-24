namespace TradeUnionCommittee.BLL.DTO
{
    public abstract class BaseUpdateDTO
    {
        public virtual string HashIdMain { get; set; }
        public virtual uint RowVersion { get; set; }
    }

    //---------------------------------------------------

    public class SubdivisionDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public override uint RowVersion { get; set; }
    }

    //---------------------------------------------------

    public class CreateSubdivisionDTO
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class CreateSubordinateSubdivisionDTO : CreateSubdivisionDTO
    {
        public string HashIdMain { get; set; }
    }

    //---------------------------------------------------

    public class UpdateSubdivisionNameDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    public class UpdateSubdivisionAbbreviationDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public string Abbreviation { get; set; }
        public override uint RowVersion { get; set; }
    }

    public class RestructuringSubdivisionDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public string HashIdSubordinate { get; set; }
        public override uint RowVersion { get; set; }
    }
}
