using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRoomBookings.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRoomBookings.Queries
{
    public class GetHotelRoomAvailabilityQuery : BasePagination, IRequest<PaginationResult<HotelRoomAvailabilityVM>>
    {
        public GetHotelRoomAvailabilityQuery(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }
    }

    public class GetHotelRoomAvailabilityQueryHandler : IRequestHandler<GetHotelRoomAvailabilityQuery, PaginationResult<HotelRoomAvailabilityVM>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetHotelRoomAvailabilityQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<HotelRoomAvailabilityVM>> Handle(GetHotelRoomAvailabilityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.HotelRooms.Include(x => x.HotelRoomBookings)
                                                 .Where(x => !x.HotelRoomBookings.Any(y => y.BookingDate.Date == request.Date.Date));

                var data = await query.OrderByDescending(x => x.RoomNumber)
                                      .Skip(request.ConvertPageToOffset())
                                      .Take(request.Size)
                                      .Select(x => new HotelRoomAvailabilityVM
                                      {
                                          Id = x.Id,
                                          RoomNumber = x.RoomNumber,
                                          Type = x.Type,
                                          Floor = x.Floor,
                                          Price = x.Price,
                                      })
                                      .ToListAsync();

                return new PaginationResult<HotelRoomAvailabilityVM>
                {
                    Data = data,
                    Page = request.Page,
                    Size = request.Size,
                    TotalRecords = await query.CountAsync()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
