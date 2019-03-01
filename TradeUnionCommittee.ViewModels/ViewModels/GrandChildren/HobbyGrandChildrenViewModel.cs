using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.GrandChildren
{
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