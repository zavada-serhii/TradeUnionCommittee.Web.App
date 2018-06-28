using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.Web.GUI.Oops
{
    public class Oops : Controller, IOops
    {
        public IActionResult OutPutError(string backController, string backAction, List<Error> errors)
        {
            ViewData["BackController"] = backController;
            ViewData["BackAction"] = backAction;
            return View("Oops");
        }
    }
}
