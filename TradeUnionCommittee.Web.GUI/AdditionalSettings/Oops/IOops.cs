using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops
{
    public interface IOops
    {
        IActionResult OutPutError(string backController, string backAction, IEnumerable<string> errors);
    }
}
