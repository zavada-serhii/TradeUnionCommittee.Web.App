using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.IO.Compression;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.Razor.Web.GUI.Configurations;
using TradeUnionCommittee.Razor.Web.GUI.Controllers.Directory;
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

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "TradeUnionCommitteeCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

builder.Services
    .AddTradeUnionCommitteeServiceModule(
        builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>(),
        builder.Configuration.GetSection("CloudStorageConnection").Get<CloudStorageConnection>(),
        builder.Configuration.GetSection("DataAnalysisConnection").Get<DataAnalysisConnection>(),
        builder.Configuration.GetSection("HashIdConfiguration").Get<HashIdConfiguration>(),
        typeof(TradeUnionCommittee.Razor.Web.GUI.Configurations.AutoMapperProfile))
    .AddTradeUnionCommitteeViewModelsModule();

builder.Services.AddResponseCompression()
        .AddDistributedMemoryCache()
        .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
        .AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddSession();

builder.Services.AddControllersWithViews()
        .AddTradeUnionCommitteeValidationModule();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddTransient<IDirectories, Directories>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#endregion

var app = builder.Build();

#region Configure

app.Services.GetRequiredService<ILoggerFactory>().AddSerilog();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Directory}/{id?}/{subid?}");
});

#endregion

await app.RunAsync();