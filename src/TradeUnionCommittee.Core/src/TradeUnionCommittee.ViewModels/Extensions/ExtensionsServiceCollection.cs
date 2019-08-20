using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.ViewModels.FluentValidation;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.ViewModels.ViewModels.Employee;

namespace TradeUnionCommittee.ViewModels.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddTradeUnionCommitteeViewModelsModule(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateEmployeeViewModel>, CreateEmployeeValidation>();
            services.AddTransient<IValidator<UpdateEmployeeViewModel>, UpdateEmployeeValidation>();
            services.AddTransient<IValidator<PdfReportViewModel>, PdfReportValidation>();
            services.AddTransient<IValidator<UpdatePositionEmployeesViewModel>, PositionEmployeesValidation>();
            return services;
        }
    }
}
