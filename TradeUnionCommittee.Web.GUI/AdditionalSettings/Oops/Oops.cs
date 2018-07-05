using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops
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
