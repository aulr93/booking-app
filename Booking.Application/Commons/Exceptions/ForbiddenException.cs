using System;

namespace Booking.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        private const string _baseMessage = "You're forbidden to access this";
        private readonly string _errorCode;

        public ForbiddenException() : base(_baseMessage)
        {

        }

        public ForbiddenException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = _baseMessage;
            }
        }

        public ForbiddenException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = _baseMessage;
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeForbidden : _errorCode;
    }
}
