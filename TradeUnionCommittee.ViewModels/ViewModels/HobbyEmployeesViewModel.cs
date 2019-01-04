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

    public class CreateHobbyChildrensViewModel
    {
        [Required]
        public string HashIdChildren { get; set; }
        [Required]
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyChildrensViewModel : CreateHobbyChildrensViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateHobbyGrandChildrensViewModel
    {
        [Required]
        public string HashIdGrandChildren { get; set; }
        [Required]
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyGrandChildrensViewModel : CreateHobbyGrandChildrensViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}