﻿namespace Booking.WebApi.Common
{
    public static class CorsServiceCollection
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition")
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}