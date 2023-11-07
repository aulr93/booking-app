using Booking.Application.Commons.Models;
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
        private DateTime _start;

        protected IMediator Mediator
        {
            get
            {
                return _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
            }
        }

        public BaseController()
        {
            _start = DateTime.Now;
        }

        /// <summary>
        /// Wrapper response
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
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

            result.ExecutionTime = DateTime.Now - _start;

            return Json(result);
        }
    }

    public enum WrapperType
    {
        OK,

        Created
    }
}
