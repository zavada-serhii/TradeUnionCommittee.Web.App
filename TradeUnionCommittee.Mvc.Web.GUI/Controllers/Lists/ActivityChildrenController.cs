using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    //IActivityChildrenService
    //ActivityChildrenDTO
    //CreateActivityChildrenViewModel
    //UpdateActivityChildrenViewModel

    public class ActivityChildrenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}