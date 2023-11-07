using Booking.Application;
using Booking.Common.Interfaces;
using Booking.Presistence;
using Booking.WebApi.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
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
//builder.Services.Configure<WebAppConfig>(builder.Configuration.GetSection(nameof(WebAppConfig)));
//builder.Services.AddScoped<ApplicationJwtManager>();

builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("id-ID")
    };

    opt.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions.Value);

app.UseAuthorization();

app.MapControllers();

app.Run();
