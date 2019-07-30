using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace TradeUnionCommittee.ViewModels.Extensions
{
    public static class ExtensionsMvcBuilder
    {
        public static IMvcBuilder AddTradeUnionCommitteeValidationModule(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddFluentValidation();
        }
    }
}
