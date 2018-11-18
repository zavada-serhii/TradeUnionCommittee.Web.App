using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Mvc.Web.GUI.Models
{
    public class DepartmentalViewModel
    {
        public string HashId { get; set; }
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Номер дому не може бути порожнім!")]
        public string NumberHouse { get; set; }
        public uint RowVersion { get; set; }
    }
}
