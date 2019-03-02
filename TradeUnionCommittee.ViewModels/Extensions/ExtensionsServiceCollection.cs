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
            services.AddScoped<IValidator<CreateEmployeeViewModel>, CreateEmployeeValidation>();
            services.AddScoped<IValidator<UpdateEmployeeViewModel>, UpdateEmployeeValidation>();
            services.AddScoped<IValidator<PdfReportViewModel>, PdfReportValidation>();
            services.AddScoped<IValidator<UpdatePositionEmployeesViewModel>, PositionEmployeesValidation>();
            return services;
        }
    }
}
