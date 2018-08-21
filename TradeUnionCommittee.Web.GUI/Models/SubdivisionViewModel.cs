using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class SubdivisionViewModel : BaseDirectoryViewModel
    {
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }
    }

    public class UpdateSubdivisionViewModel : BaseDirectoryViewModel
    {
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateAbbreviationSubdivisionViewModel : BaseDirectoryViewModel
    {
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Aбревіатура не може бути порожньою!")]
        [Remote("CheckAbbreviation", "Subdivision", ErrorMessage = "Ця aбревіатура вже використовується!")]
        public string Abbreviation { get; set; }
    }
}