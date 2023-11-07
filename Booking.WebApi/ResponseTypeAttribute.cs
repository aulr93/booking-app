using Booking.Application.Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi
{
    public class ResponseTypeAttribute : ProducesResponseTypeAttribute
    {
        public ResponseTypeAttribute(Type type, int statusCode) : base((typeof(Result<>)).MakeGenericType(type), statusCode) { }
    }
}
