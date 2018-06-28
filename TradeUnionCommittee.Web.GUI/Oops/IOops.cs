using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.Web.GUI.Oops
{
    public interface IOops
    {
        IActionResult OutPutError(string backController, string backAction, List<Error> errors);
    }
}
