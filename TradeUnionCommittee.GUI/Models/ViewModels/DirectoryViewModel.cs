using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.GUI.Models.ViewModels
{
    public class DirectoryViewModel
    {
        public long? Id { get; set; }
        [Remote("CheckName", "Position", ErrorMessage = "Це назва вже використовується!")]
        public string Name { get; set; }
    }
}
