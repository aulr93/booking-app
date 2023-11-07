using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Helpers;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Application.Features.HotelRooms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRoomBookings.Queries
{
    public class GetDetailHotelRoomBookingQuery : IRequest<HotelRoomBookingVM>
    {
        public GetDetailHotelRoomBookingQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetDetailHotelRoomBookingQueryHandler : IRequestHandler<GetDetailHotelRoomBookingQuery, HotelRoomBookingVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly MessageLanguage _messageLanguage;

        public GetDetailHotelRoomBookingQueryHandler(IApplicationDbContext dbContext,
            MessageLanguage messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<HotelRoomBookingVM> Handle(GetDetailHotelRoomBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.hotelRoomBookings.Include(x => x.HotelRoom)
                                                               .Where(x => x.Id == request.Id)
                                                               .Select(x => new HotelRoomBookingVM
                                                               {
                                                                   Id = x.Id,
                                                                   VisitorName = x.VisitorName,
                                                                   NIK = x.NIK,
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
                                                               }).FirstOrDefaultAsync();

                if (result == null)
                    throw new NotFoundException(_messageLanguage[MessageCodeConstant.DataNotFound]);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
