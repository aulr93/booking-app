using Booking.Common.Interfaces;
using Booking.WebApi.Common;
using Booking.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag.Generation.Processors.Security;
using NSwag;
using FluentValidation.AspNetCore;

namespace Booking.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IMachineDateTime, MachineDateTime>();

            services.AddHttpContextAccessor();

            //services.AddControllersWithViews(opt => opt.Filters.Add<ApiExceptionFilterAttribute>());
            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; })
                    .Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "API Booking";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
    }
}
