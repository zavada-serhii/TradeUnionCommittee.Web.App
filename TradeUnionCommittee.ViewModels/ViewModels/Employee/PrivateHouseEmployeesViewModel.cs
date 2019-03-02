using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public abstract class BasePrivateHouseEmployeesViewModel
    {
        [Required]
        public virtual string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public virtual string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public virtual string Street { get; set; }
        public virtual string NumberHouse { get; set; }
        public virtual string NumberApartment { get; set; }
    }

    public class CreatePrivateHouseEmployeesViewModel : BasePrivateHouseEmployeesViewModel
    {
        public override string HashIdEmployee { get; set; }
        public override string City { get; set; }
        public override string Street { get; set; }
        public override string NumberHouse { get; set; }
        public override string NumberApartment { get; set; }
    }

    public class UpdatePrivateHouseEmployeesViewModel : BasePrivateHouseEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        public override string HashIdEmployee { get; set; }
        public override string City { get; set; }
        public override string Street { get; set; }
        public override string NumberHouse { get; set; }
        public override string NumberApartment { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    public class CreateUniversityHouseEmployeesViewModel : CreatePrivateHouseEmployeesViewModel
    {
        [Required(ErrorMessage = "Дата розподілу не може бути порожньою!")]
        public DateTime DateReceiving { get; set; }
    }

    public class UpdateUniversityHouseEmployeesViewModel : UpdatePrivateHouseEmployeesViewModel
    {
        [Required(ErrorMessage = "Дата розподілу не може бути порожньою!")]
        public DateTime DateReceiving { get; set; }
    }
}