using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateSocialActivityEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required]
        public string HashIdSocialActivity { get; set; }
        public string Note { get; set; }
        [Required]
        public bool CheckSocialActivity { get; set; }
    }

    public class UpdateSocialActivityEmployeesViewModel : CreateSocialActivityEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}