using System.Text.RegularExpressions;

namespace Booking.WebApi.Helpers
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value != null ? Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2") : string.Empty;
        }
    }
}