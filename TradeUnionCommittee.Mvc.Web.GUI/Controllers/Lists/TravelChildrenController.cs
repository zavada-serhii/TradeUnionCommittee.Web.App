using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    //ITravelChildrenService
    //TravelChildrenDTO
    //CreateEventChildrenViewModel
    //UpdateEventChildrenViewModel

    public class TravelChildrenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}