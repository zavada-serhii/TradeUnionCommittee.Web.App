using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace TradeUnionCommittee.Api.Attributes
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
            context.Result = new BadRequestObjectResult(errors);
        }
    }
}
