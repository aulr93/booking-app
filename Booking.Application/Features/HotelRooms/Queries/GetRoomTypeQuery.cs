using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Models;
using MediatR;

namespace Booking.Application.Features.HotelRooms.Queries
{
    public class GetRoomTypeQuery : BasePagination, IRequest<List<string>>
    {
        public GetRoomTypeQuery()
        {
        }
    }

    public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, List<string>>
    {
        public GetRoomTypeQueryHandler()
        {
        }

        public async Task<List<string>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(ApplicationConstant.RoomType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
