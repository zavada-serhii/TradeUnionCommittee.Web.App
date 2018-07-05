using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class DirectoryViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
    }
}
