using Booking.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booking.WebApi.Exceptions
{
    public class ModelStateValidationException : Exception
    {
        public ModelStateValidationException(ModelStateDictionary modelState) : base("One or more validation failures have occured.")
        {
            Failures = new Dictionary<string, string[]>();

            var errors = modelState.Where(n => n.Value.Errors.Count > 0).ToList();
            foreach (var item in errors)
            {
                Failures.Add(key: item.Key, value: item.Value.Errors.Select(x => x.ErrorMessage).ToArray());
            }
        }

        public IDictionary<string, string[]> Failures { get; }

        public string ErrorCode => ErrorCodeConstant.ErrorCodeInvalidValidation;
    }
}
