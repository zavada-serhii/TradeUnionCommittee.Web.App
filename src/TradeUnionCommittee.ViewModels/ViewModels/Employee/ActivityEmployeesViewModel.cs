using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreateActivityEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Назва заходу не може бути порожньою!")]
        public string HashIdActivities { get; set; }
        [Required(ErrorMessage = "Дата проведення не може бути порожньою!")]
        public DateTime DateEvent { get; set; }
    }

    public class UpdateActivityEmployeesViewModel : CreateActivityEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}