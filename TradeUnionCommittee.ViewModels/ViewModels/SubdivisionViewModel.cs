using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateMainSubdivisionViewModel
    {
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }
    }

    public class CreateSubordinateSubdivisionViewModel
    {
        [Required]
        public string HashIdSubordinate { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }
    }

    public class UpdateNameSubdivisionViewModel 
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    public class UpdateAbbreviationSubdivisionViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    public class DeleteSubdivisionViewModel
    {
        public string HashId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class RestructuringViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public string HashIdSubordinate { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}