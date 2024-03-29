﻿using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Services;
using Booking.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class BookingRoomCommand : IRequest<Unit>
    {
        public BookingRoomCommand(Guid roomId, DateTime bookingDate)
        {
            RoomId = roomId;
            BookingDate = bookingDate;
        }

        public Guid RoomId { get; set; }
        public DateTime BookingDate { get; set; }
    }

    public class BookingRoomCommandHandler : IRequestHandler<BookingRoomCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMachineDateTime _dateTime;
        private readonly IMessageLanguageService _messageLanguage;
        private readonly ICurrentUserService _currentUserService;

        public BookingRoomCommandHandler(IApplicationDbContext dbContext,
            IMachineDateTime dateTime,
            IMessageLanguageService messageLanguage,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
            _messageLanguage = messageLanguage;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(BookingRoomCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var hotelRoom = await _dbContext.HotelRooms.Include(x => x.HotelRoomBookings)
                                                           .FirstOrDefaultAsync(x => x.Id == request.RoomId && 
                                                                                     !x.HotelRoomBookings.Any(y => y.BookingDate == _dateTime.UtcNow));
                if (hotelRoom is null)
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.RoomNoNotExist));

                if (hotelRoom.HotelRoomBookings is not null && hotelRoom.HotelRoomBookings.Any())
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.RoomHasBeenBooked));

                _dbContext.HotelRoomBookings.Add(new HotelRoomBooking
                {
                    Id = Guid.NewGuid(),
                    RoomId = request.RoomId,
                    VisitorId = Guid.Parse(_currentUserService.UserId),
                    Date = _dateTime.UtcNow,
                    BookingDate = request.BookingDate,
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
