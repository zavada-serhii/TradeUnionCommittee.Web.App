using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TradeUnionCommittee.Web.GUI.Oops
{
    public interface IOops
    {
        IActionResult OutPutError(string backController, string backAction, List<string> errors);
    }
}
