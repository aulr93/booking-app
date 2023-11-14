using Booking.Application.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;
using Booking.Application.Commons.Models;

namespace Booking.WebApi.Common
{
    public class ResultManipulator : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // do nothing
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //run code immediately before and after the execution of action results. They run only when the action method has executed successfully. They are useful for logic that must surround view or formatter execution.
            if (context.Result is ObjectResult result)
            {
                var resultObj = result.Value;

                var actionMetadata = context.ActionDescriptor.EndpointMetadata;
                var ignoreResultManipulatorObj = actionMetadata.FirstOrDefault(metadataItem => metadataItem is IgnoreResultManipulatorAttribute);
                if (ignoreResultManipulatorObj is IgnoreResultManipulatorAttribute ignoreResultManipulator)
                {
                    if (!ignoreResultManipulator.IgnoreSetResult)
                    {
                        context.Result = new JsonResult(resultObj, new JsonSerializerOptions()
                        {
                            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        });
                    }
                }
                else
                {
                    Result<object> resp = new Result<object>
                    {
                        IsSuccess = false,
                        Path = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : ""
                    };

                    if (resultObj is not null && resultObj is not Unit)
                        resp.Payload = resultObj;

                    context.Result = new JsonResult(resp, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        Converters = { new JsonStringEnumConverter() }
                    });
                }
            }
        }
    }
}
