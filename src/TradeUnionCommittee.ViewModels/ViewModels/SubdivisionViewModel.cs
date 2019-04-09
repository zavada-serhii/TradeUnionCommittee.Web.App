using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.Attributes;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateMainSubdivisionViewModel
    {
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        public string Abbreviation { get; set; }
    }

    public class CreateSubordinateSubdivisionViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        public string Abbreviation { get; set; }
    }

    public class UpdateNameSubdivisionViewModel 
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        public string Name { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    public class UpdateAbbreviationSubdivisionViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        public string Abbreviation { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    [RestructuringCompare]
    public class RestructuringViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required]
        public string HashIdSubordinate { get; set; }
        [Required]
        public uint RowVersionSubordinateSubdivision { get; set; }
    }
}