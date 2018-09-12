using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class PositionViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }
}
