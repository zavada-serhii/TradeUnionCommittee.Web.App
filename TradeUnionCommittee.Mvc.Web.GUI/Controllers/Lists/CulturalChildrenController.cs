using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    //ICulturalChildrenService
    //CulturalChildrenDTO
    //CreateCulturalChildrenViewModel
    //UpdateCulturalChildrenViewModel

    public class CulturalChildrenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}