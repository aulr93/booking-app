using System;

namespace Booking.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        private readonly string _errorCode;

        public BadRequestException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in BadRequestException class can not be null or empty", nameof(message));
            }
        }

        public BadRequestException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in BadRequestException class can not be null or empty", nameof(message));
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeBadRequest : _errorCode;
    }
}
