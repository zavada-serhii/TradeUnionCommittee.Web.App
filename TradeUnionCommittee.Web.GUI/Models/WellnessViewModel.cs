using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class WellnessViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Wellness", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }
}