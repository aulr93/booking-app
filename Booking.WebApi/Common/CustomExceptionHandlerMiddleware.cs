using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Models;
using Booking.WebApi.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Booking.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
           var executingEndpoint = context.GetEndpoint();

            bool hasIgnoreManipulateResult = false;
            if (executingEndpoint != null)
            {
                var att = executingEndpoint.Metadata.OfType<IgnoreResultManipulatorAttribute>();
                if (att.Any())
                    hasIgnoreManipulateResult = true;
            }

            Result<object> result = new Result<object>
            {
                IsSuccess = false,
                Path = context.Request.Path.HasValue ? context.Request.Path.Value : ""
            };

            bool errorBecauseOfValidationException = false;
            var logError = false;
            switch (ex)
            {
                case ValidationException validationException:
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    result.Payload = validationException.Failures;
                    result.Message = validationException.Message;
                    errorBecauseOfValidationException = true;
                    break;
                case BadRequestException badRequestException:
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    result.Message = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Message = notFoundException.Message;
                    break;
                case UnauthorizeException unauthorizeException:
                    result.StatusCode = (int)HttpStatusCode.Unauthorized;
                    result.Message = unauthorizeException.Message;
                    break;
                case ForbiddenException forbiddenException:
                    result.StatusCode = (int)HttpStatusCode.Forbidden;
                    result.Message = forbiddenException.Message;
                    break;
                default:
                    logError = true;
                    result.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result.Message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        result.InnerMessage = ex.InnerException.Message;
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            var jsonSetting = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (errorBecauseOfValidationException)
                jsonSetting.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;

            if (!hasIgnoreManipulateResult)
                return context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonSetting));
            else
                return context.Response.WriteAsync(JsonSerializer.Serialize(result.Message));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
