using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Helpers;
using Booking.Application.Commons.Interfaces;
using Booking.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class BookingRoomCommand : IRequest<Unit>
    {
        public BookingRoomCommand(Guid roomId, string visitorName, string nik, DateTime bookingDate, DateTime? checkInDate)
        {
            RoomId = roomId;
            VisitorName = visitorName;
            NIK = nik;
            BookingDate = bookingDate;
            CheckInDate = checkInDate;
        }

        public Guid RoomId { get; set; }
        public string VisitorName { get; set; }
        public string NIK { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
    }

    public class BookingRoomCommandHandler : IRequestHandler<BookingRoomCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMachineDateTime _dateTime;
        private readonly MessageLanguage _messageLanguage;

        public BookingRoomCommandHandler(IApplicationDbContext dbContext,
            IMachineDateTime dateTime,
            MessageLanguage messageLanguage)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(BookingRoomCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var hotelRoom = await _dbContext.hotelRooms.Include(x => x.HotelRoomBookings)
                                                           .FirstOrDefaultAsync(x => x.Id == request.RoomId && 
                                                                                     x.HotelRoomBookings.Any(y => y.BookingDate == _dateTime.UtcNow));
                if (hotelRoom is null)
                    throw new BadRequestException(_messageLanguage[MessageCodeConstant.RoomNoNotExist]);

                if (hotelRoom.HotelRoomBookings is not null && hotelRoom.HotelRoomBookings.Any())
                    throw new BadRequestException(_messageLanguage[MessageCodeConstant.RoomHasBeenBooked]);

                _dbContext.hotelRoomBookings.Add(new HotelRoomBooking
                {
                    Id = Guid.NewGuid(),
                    RoomId = request.RoomId,
                    VisitorName = request.VisitorName,
                    NIK = request.NIK,
                    Date = _dateTime.UtcNow,
                    BookingDate = request.BookingDate,
                    ActualCheckInDate = request.CheckInDate.HasValue ? request.CheckInDate : null,
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception(ex.Message);
            }
        }
    }
}
