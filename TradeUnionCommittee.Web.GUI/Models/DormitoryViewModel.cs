using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class DormitoryViewModel
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Номер дому не може бути порожнім!")]
        public string NumberHouse { get; set; }
        [Required(ErrorMessage = "Номер гуртожитку не може бути порожнім!")]
        public string NumberDormitory { get; set; }
    }
}
