using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Models.ViewModels
{
    public class DirectoryViewModel
    {
        public long? Id { get; set; }
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public string Name { get; set; }
    }
}
