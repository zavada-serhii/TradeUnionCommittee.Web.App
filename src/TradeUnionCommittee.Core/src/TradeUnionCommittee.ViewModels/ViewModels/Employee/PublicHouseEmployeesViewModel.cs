using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreatePublicHouseEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Адреса не може бути порожньою!")]
        public string HashIdAddressPublicHouse { get; set; }
        [Required(ErrorMessage = "Номер кімнати не може бути порожнім!")]
        public string NumberRoom { get; set; }
    }

    public class UpdatePublicHouseEmployeesViewModel : CreatePublicHouseEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}