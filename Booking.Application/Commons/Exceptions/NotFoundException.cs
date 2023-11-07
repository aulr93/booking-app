using System;

namespace Booking.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly string _errorCode;

        public NotFoundException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in NotFoundException class can not be null or empty", nameof(message));
            }
        }

        public NotFoundException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in NotFoundException class can not be null or empty", nameof(message));
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeNotFound : _errorCode;
    }
}
