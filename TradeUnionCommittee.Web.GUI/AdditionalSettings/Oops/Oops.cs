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
            ViewBag.Error = UkrainianError(errors);
            return View("Oops");
        }


        private string UkrainianError(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                switch (error)
                {
                    case "0001":
                        return "Запис вже видалено іншим користувачем!";
                    case "0002":
                        return "Запис вже був оновлений іншим користувачем!";
                    case "0003":
                        return "Недійсний ідентифікатор!";
                    case "0004":
                        return "Такий запис вже існує!";
                    default:
                        return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
