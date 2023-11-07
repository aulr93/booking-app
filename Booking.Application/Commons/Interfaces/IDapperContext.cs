using System.Data.Common;

namespace Booking.Application.Commons.Interfaces
{
    public interface IDapperContext
    {
        DbConnection CreateConnection();
    }
}
