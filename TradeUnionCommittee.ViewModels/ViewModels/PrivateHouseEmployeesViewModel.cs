using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreatePrivateHouseEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public virtual string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public virtual string Street { get; set; }
        public virtual string NumberHouse { get; set; }
        public virtual string NumberApartment { get; set; }
    }

    public class UpdatePrivateHouseEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public virtual string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public virtual string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberApartment { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
