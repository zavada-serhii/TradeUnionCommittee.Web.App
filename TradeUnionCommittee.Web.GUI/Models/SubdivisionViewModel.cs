using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class SubdivisionViewModel : DirectoryViewModel
    {
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Subdivision", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }
}
