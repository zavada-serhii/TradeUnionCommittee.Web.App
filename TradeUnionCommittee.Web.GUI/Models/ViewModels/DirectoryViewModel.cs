using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models.ViewModels
{
    public class DirectoryViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
    }
}
