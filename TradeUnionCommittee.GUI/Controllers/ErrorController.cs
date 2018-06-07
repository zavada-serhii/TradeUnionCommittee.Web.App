using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.GUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult OutPutError(string backController, string backAction, List<Error> errors)
        {
            ViewData["BackController"] = backController;
            ViewData["BackAction"] = backAction;
            ViewData["ErrorMessage"] = errors;
            return View("DataBaseError");
        }
    }
}
