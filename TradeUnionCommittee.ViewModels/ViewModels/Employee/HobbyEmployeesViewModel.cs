using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreateHobbyEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required]
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyEmployeesViewModel : CreateHobbyEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}