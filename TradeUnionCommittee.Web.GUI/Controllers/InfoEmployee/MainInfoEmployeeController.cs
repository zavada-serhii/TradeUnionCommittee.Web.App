using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Controllers.InfoEmployee
{
    public class MainInfoEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}