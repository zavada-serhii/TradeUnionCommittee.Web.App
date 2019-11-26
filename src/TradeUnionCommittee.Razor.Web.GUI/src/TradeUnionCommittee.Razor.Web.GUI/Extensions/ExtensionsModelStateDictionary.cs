using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace TradeUnionCommittee.Razor.Web.GUI.Extensions
{
    public static class ExtensionsModelStateDictionary
    {
        public static IEnumerable<string> GetErrors(this ModelStateDictionary modelState) =>
            modelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
    }
}