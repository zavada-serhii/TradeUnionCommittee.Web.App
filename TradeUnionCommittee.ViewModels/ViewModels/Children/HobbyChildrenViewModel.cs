using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Children
{
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
}