using Autofac.Core;
using Booking.Application;
using Booking.Application.Commons.Extensions;
using Booking.Common.Interfaces;
using Booking.Presistence;
using Booking.WebApi;
using Booking.WebApi.Commons;
using Booking.WebApi.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommandLine(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

//var webAppConfig = new WebAppConfig();
//builder.Configuration.GetSection(nameof(WebAppConfig)).Bind(webAppConfig);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IMachineDateTime, MachineDateTime>();
builder.Services.AddHttpClient();

builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddJsonWebTokenService(builder.Configuration);

builder.Services.AddLocalization();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<AcceptLanguageHeaderParameter>();
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
