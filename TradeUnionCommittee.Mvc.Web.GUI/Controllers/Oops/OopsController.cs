using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Oops
{
    public interface IOops
    {
        IActionResult OutPutError(string backController, string backAction, IEnumerable<string> errors);
    }

    public class OopsController : Controller, IOops
    {
        public IActionResult OutPutError(string backController, string backAction, IEnumerable<string> errors)
        {
            ViewData["BackController"] = backController;
            ViewData["BackAction"] = backAction;
            ViewBag.Errors = errors;
            return View("Oops");
        }
    }
}