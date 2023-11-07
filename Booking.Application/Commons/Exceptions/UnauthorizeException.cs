using System;

namespace Booking.Application.Common.Exceptions
{
    public class UnauthorizeException : Exception
    {
        private const string _baseMessage = "You're not authorized";

        private readonly string _errorCode;

        public UnauthorizeException() : base(_baseMessage)
        {
        }

        public UnauthorizeException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = _baseMessage;
            }
        }

        public UnauthorizeException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = _baseMessage;
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeUnauthorized : _errorCode;
    }
}
