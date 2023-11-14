using AutoMapper;
using Booking.Application.Commons.Models;
using Booking.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private IMediator _mediator;
        private ApplicationJwtManager _applicationJwtManager;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ApplicationJwtManager JwtManager => _applicationJwtManager ??= HttpContext.RequestServices.GetService<ApplicationJwtManager>();
        protected IMapper Mapper => HttpContext.RequestServices.GetService<IMapper>();

        protected JsonResult Wrapper(object val, WrapperType wrapperType = WrapperType.OK)
        {
            Result<object> result = new Result<object>();

            result.Path = Request.Path.HasValue ? Request.Path.Value : null;

            if (val != null && !(val is Unit))
            {
                result.Payload = val;
            }

            if (wrapperType == WrapperType.Created)
            {
                result.StatusCode = (int)HttpStatusCode.Created;
            }

            return Json(result);
        }
    }

    public enum WrapperType
    {
        OK,

        Created
    }
}
