using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreateApartmentAccountingEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Кількість членів сім'ї не може бути порожнім!")]
        public long FamilyComposition { get; set; }
        [Required(ErrorMessage = "Назва адміністрації не може бути порожньою!")]
        public string NameAdministration { get; set; }
        [Required(ErrorMessage = "Черговість не може бути порожньою!")]
        public string PriorityType { get; set; }
        [Required(ErrorMessage = "Дата прийняття на облік не може бути порожньою!")]
        public DateTime DateAdoption { get; set; }
        public DateTime? DateInclusion { get; set; }
        [Required(ErrorMessage = "Посада не може бути порожньою!")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Рік початку роботи в ОНУ не може бути порожнім!")]
        public int StartYearWork { get; set; }
    }

    public class UpdateApartmentAccountingEmployeesViewModel : CreateApartmentAccountingEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}