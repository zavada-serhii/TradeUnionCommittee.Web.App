using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    //IGiftChildrenService
    //GiftChildrenDTO
    //CreateGiftChildrenViewModel
    //UpdateGiftChildrenViewModel

    public class GiftChildrenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}