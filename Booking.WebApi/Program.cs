using Booking.Application;
using Booking.Common;
using Booking.Presistence;
using Booking.WebApi;
using Booking.WebApi.Common;
using Booking.WebApi.Helpers;
using Booking.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommandLine(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

var webAppConfig = new WebAppConfig();
builder.Configuration.GetSection(nameof(WebAppConfig)).Bind(webAppConfig);
builder.Services.Configure<WebAppConfig>(builder.Configuration.GetSection(nameof(WebAppConfig)));

//builder.Services.AddHttpClient();
builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddTransient<CustomJwtAuthenticationHandler>();
builder.Services.AddScoped<ApplicationJwtManager>();

builder.Services.AddLocalization();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ResultManipulator());

    //options.Filters.Add(typeof(ModelValidationActionExecutingFilter));
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

builder.Services.AddCorsConfiguration();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CustomJwtAuthenticationOptions.DefaultSchemeName;
    o.DefaultAuthenticateScheme = CustomJwtAuthenticationOptions.DefaultSchemeName;
})
.AddScheme<CustomJwtAuthenticationOptions, CustomJwtAuthenticationHandler>(CustomJwtAuthenticationOptions.DefaultSchemeName, opts => { });

builder.Services.AddAuthorization(opts =>
{
    opts.DefaultPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(CustomJwtAuthenticationOptions.DefaultSchemeName)
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddSwaggerVersioning();

//------------------------------------ app builder ------------------------------------
var app = builder.Build();

string pathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATH_BASE") ?? string.Empty;

var supportedCultures = new[] { "en-US", "id-ID" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                                                          .AddSupportedCultures(supportedCultures)
                                                          .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UsePathBase(pathBase);

    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            swagger.Servers = new List<OpenApiServer>
            {
                new OpenApiServer
                {
                    Url = !string.IsNullOrEmpty(pathBase) ? pathBase : $"{httpReq.Scheme}://{httpReq.Host.Value}"
                }
            };
        });
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "v1");
        c.SwaggerEndpoint($"{pathBase}/swagger/v2/swagger.json", "v2");
    });
}

app.UseCustomExceptionHandler();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
