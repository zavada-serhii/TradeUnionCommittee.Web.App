using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TradeUnionCommittee.Web.GUI.Oops
{
    public class Oops : Controller, IOops
    {
        public IActionResult OutPutError(string backController, string backAction, List<string> errors)
        {
            ViewData["BackController"] = backController;
            ViewData["BackAction"] = backAction;
            return View("Oops");
        }
    }
}
