using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreatePrivilegeEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required]
        public string HashIdPrivileges { get; set; }
        public string Note { get; set; }
        [Required]
        public bool CheckPrivileges { get; set; }
    }

    public class UpdatePrivilegeEmployeesViewModel : CreatePrivilegeEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}