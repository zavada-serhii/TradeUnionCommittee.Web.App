using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class UpdatePersonalDataAccountViewModel
    {
        public long? IdUser { get; set; }

        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Цей email вже використовується!")]
        public string Email { get; set; }

        public long IdRole { get; set; }
    }
}