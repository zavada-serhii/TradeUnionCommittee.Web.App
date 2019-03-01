using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
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

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateHobbyChildrenViewModel
    {
        [Required]
        public string HashIdChildren { get; set; }
        [Required]
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyChildrenViewModel : CreateHobbyChildrenViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateHobbyGrandChildrenViewModel
    {
        [Required]
        public string HashIdGrandChildren { get; set; }
        [Required]
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyGrandChildrenViewModel : CreateHobbyGrandChildrenViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}