using Booking.Application.Commons.Authentications;
using Booking.Application.Commons.Authentications.JsonWebTokens;
using Booking.Application.Commons.Behaviours;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Booking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
            services.AddScoped<IJwtProvider, JwtService>();
            services.AddScoped<IMessageLanguageService, MessageLanguageService>();

            return services;
        }
    }
}