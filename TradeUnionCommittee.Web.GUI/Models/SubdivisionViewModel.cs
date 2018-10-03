using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
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
        public string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }

        public uint RowVersion { get; set; }
    }

    public class UpdateAbbreviationSubdivisionViewModel
    {
        public string HashId { get; set; }

        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }

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
        public uint RowVersion { get; set; }

    }
}