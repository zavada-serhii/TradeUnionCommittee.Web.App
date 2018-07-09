using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class WellnessViewModel : DirectoryViewModel
    {
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Wellness", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }
}