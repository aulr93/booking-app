using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRooms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRoomBookings.Queries
{
    public class GetHotelRoomBookingQuery : BasePagination, IRequest<PaginationResult<HotelRoomBookingVM>>
    {
        public GetHotelRoomBookingQuery(DateTime date, EnumGetRoomData getRoomData)
        {
            Date = date;
            GetRoomData = getRoomData;
        }

        public DateTime Date { get; }
        public EnumGetRoomData GetRoomData { get; }
    }

    public class GetHotelRoomBookingQueryHandler : IRequestHandler<GetHotelRoomBookingQuery, PaginationResult<HotelRoomBookingVM>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetHotelRoomBookingQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<HotelRoomBookingVM>> Handle(GetHotelRoomBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.HotelRoomBookings.Include(x => x.HotelRoom)
                                                        .Where(x => x.BookingDate == request.Date);

                var data = await query.OrderByDescending(x => x.BookingDate)
                                      .Skip(request.ConvertPageToOffset())
                                      .Take(request.Size)
                                      .Select(x => new HotelRoomBookingVM
                                      {
                                          Id = x.Id,
                                          VisitorName = string.Empty,
                                          NIK = string.Empty,
                                          Date = x.Date,
                                          BookingDate = x.BookingDate,
                                          ActualCheckInDate = x.ActualCheckInDate,
                                          ActualCheckOutDate = x.ActualCheckOutDate,
                                          RoomDetail = new HotelRoomVM
                                          {
                                              Id = x.HotelRoom.Id,
                                              RoomNumber = x.HotelRoom.RoomNumber,
                                              Type = x.HotelRoom.Type,
                                              Floor = x.HotelRoom.Floor,
                                              Price = x.HotelRoom.Price
                                          }
                                      })
                                      .ToListAsync();

                return new PaginationResult<HotelRoomBookingVM>
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
