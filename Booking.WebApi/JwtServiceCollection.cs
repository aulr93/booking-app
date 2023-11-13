using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Authentications.JsonWebTokens;
using Booking.Application.Commons.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Booking.WebApi
{
    public static class JwtServiceCollection
    {
        /// <summary>
        /// Melakukan seluruh setup authentication menggunakan Json Web Token. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJsonWebTokenService(this IServiceCollection services, IConfiguration configuration, string configName = "JwtOption")
        {
            JwtOption option = new JwtOption();
            services.Configure<JwtOption>(configuration.GetSection(configName));

            services.AddScoped<IJwtProvider, JwtService>();

            JwtOptionValidator validator = new JwtOptionValidator();
            configuration.GetSection(configName).Bind(option);
            var validationResult = validator.Validate(option);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Failed to initiate AddJsonWebTokenService due to invalid dependecies. Please check JwtOption in appsettings");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // add token to authentication properties
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(option.SecretKey)),

                    ValidateIssuer = false,
                    //ValidIssuer

                    ValidateAudience = false,
                    //ValidAudience

                    ValidateLifetime = true,

                    // default is 5 minutes, set it to 0
                    ClockSkew = new TimeSpan(0)
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        await Task.Run(() => throw new UnauthorizeException());
                    },

                    OnForbidden = async context =>
                    {
                        await Task.Run(() => throw new ForbiddenException());
                    },

                    OnAuthenticationFailed = async context =>
                    {
                        await Task.Run(() => throw new UnauthorizeException());
                    },

                    OnTokenValidated = async context =>
                    {
                        var principal = context.Principal;

                        // get name identifier from principal
                        var nameIdentifier = await Task.Run(() => principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier));

                        var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
                    }
                };
            });

            return services;
        }
    }
}
