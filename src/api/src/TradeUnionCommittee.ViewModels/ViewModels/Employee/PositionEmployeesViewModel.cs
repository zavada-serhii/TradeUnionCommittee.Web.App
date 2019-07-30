using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class UpdatePositionEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public string HashIdEmployee { get; set; }
        [Required]
        public string HashIdSubdivision { get; set; }
        [Required]
        public string HashIdPosition { get; set; }
        [Required]
        public bool CheckPosition { get; set; }
        [Required(ErrorMessage = "Дата початку не може бути порожньою!")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
