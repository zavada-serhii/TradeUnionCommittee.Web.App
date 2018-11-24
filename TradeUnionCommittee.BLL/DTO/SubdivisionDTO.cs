namespace TradeUnionCommittee.BLL.DTO
{
    public class SubdivisionDTO : BaseDTO
    {
        public string HashIdMain { get; set; }
        public string HashIdSubordinate { get; set; }
        public override string Name { get; set; }
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
    
    public class UpdateSubdivisionDTO
    {
        public string HashIdMain { get; set; }
        public string Name { get; set; }
        public uint RowVersion { get; set; }
    }

    //---------------------------------------------------

    public class Get : UpdateSubdivisionDTO
    {
        public string Abbreviation { get; set; }
    }

    //---------------------------------------------------

    public class RestructuringSubdivisionDTO
    {
        public string HashIdMain { get; set; }
        public string HashIdSubordinate { get; set; }
        public uint RowVersion { get; set; }
    }
}
