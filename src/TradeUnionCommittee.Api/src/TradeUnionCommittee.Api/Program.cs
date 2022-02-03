using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO.Compression;
using TradeUnionCommittee.Api.Configurations;
using TradeUnionCommittee.Api.Extensions;
using TradeUnionCommittee.Api.Filters;
using TradeUnionCommittee.Api.Models;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.ViewModels.Extensions;

#region Builder

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
               .SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", reloadOnChange: true, optional: true)
               .AddEnvironmentVariables();

#endregion

#region Logger

var elk = builder.Configuration.GetSection("ElkConnection").Get<ElkConnection>();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elk.Url))
    {
        AutoRegisterTemplate = true,
        ModifyConnectionSettings = settings =>
        {
            if (elk.UseBasicAuthentication)
                settings.BasicAuthentication(elk.UserName, elk.Password);

            if (elk.IgnoreCertificateValidation)
                settings.ServerCertificateValidationCallback((sender, certificate, chain, errors) => true);

            return settings;
        }
    })
    .WriteTo.Console(LogEventLevel.Information)
    .CreateLogger();

#endregion

#region Configure Services

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthModel>();
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = authOptions.Issuer,

        ValidateAudience = true,
        ValidAudience = authOptions.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddTradeUnionCommitteeServiceModule(
        builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>(),
        builder.Configuration.GetSection("CloudStorageConnection").Get<CloudStorageConnection>(),
        builder.Configuration.GetSection("DataAnalysisConnection").Get<DataAnalysisConnection>(),
        builder.Configuration.GetSection("HashIdConfiguration").Get<HashIdConfiguration>(),
        typeof(TradeUnionCommittee.Api.Configurations.AutoMapperProfile))
    .AddTradeUnionCommitteeViewModelsModule();

builder.Services.AddResponseCompression()
        .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
        .AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddApiVersioning(options => options.ReportApiVersions = true)
        .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; options.SubstituteApiVersionInUrl = true; })
        .AddSwaggerGen(x =>
        {
            x.SchemaFilter<DefaultValueSchemaFilter>();
            x.UseAllOfToExtendReferenceSchemas();
        });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers()
        .AddTradeUnionCommitteeValidationModule();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});


builder.Services.AddOptions();
builder.Services.Configure<AuthModel>(builder.Configuration.GetSection("AuthOptions"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IJwtBearerConfiguration, JwtBearerConfiguration>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

#endregion

var app = builder.Build();

#region Configure

app.Services.GetRequiredService<ILoggerFactory>().AddSerilog();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseResponseCompression();
app.UseCustomMiddlewares();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
    c.DocExpansion(DocExpansion.None);
    c.EnableValidator();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#endregion

await app.RunAsync();