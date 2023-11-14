using Microsoft.OpenApi.Models;

namespace Booking.WebApi.Common
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerVersioning(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                //opt.OperationFilter<AcceptLanguageHeaderParameter>();
                opt.OperationFilter<AddAuthorizationHeaderOperationFilter>();
                opt.OperationFilter<AddDefaultPaginationQueryParametersOperationFilter>();

                opt.SwaggerDoc(
                    name: "v1",
                    info: new OpenApiInfo
                    {
                        Title = $"API Booking",
                        Version = "v1",
                        Description = "Api Project created by anonymous"
                    });

                opt.AddSecurityDefinition(
                    name: "Bearer",
                    securityScheme: new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                    });
            });

            return services;
        }
    }
}
