namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateHobbyEmployeesViewModel
    {
        public string HashIdEmployee { get; set; }
        public string HashIdHobby { get; set; }
    }

    public class UpdateHobbyEmployeesViewModel : CreateHobbyEmployeesViewModel
    {
        public string HashId { get; set; }
        public uint RowVersion { get; set; }
    }
}