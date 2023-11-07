using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Helpers;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Features.HotelRoomBookings.Models;
using Booking.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class CheckInOutCommand : IRequest<Unit>
    {
        public CheckInOutCommand(Guid id, EnumCheckInOut checkInOut)
        {
            Id = id;
            CheckInOut = checkInOut;
        }

        public Guid Id { get; set; }
        public EnumCheckInOut CheckInOut { get; set; }
    }

    public class CheckInOutCommandHandler : IRequestHandler<CheckInOutCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMachineDateTime _dateTime;
        private readonly MessageLanguage _messageLanguage;

        public CheckInOutCommandHandler(IApplicationDbContext dbContext,
            IMachineDateTime dateTime,
            MessageLanguage messageLanguage)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(CheckInOutCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var booking = await _dbContext.hotelRoomBookings.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (booking is null)
                    throw new BadRequestException(_messageLanguage[MessageCodeConstant.BookingRoomNotFound]);

                if (request.CheckInOut == EnumCheckInOut.CheckOut)
                {
                    if (booking.ActualCheckInDate is null)
                        throw new BadRequestException(_messageLanguage[MessageCodeConstant.VisitorNotCheckIn]);

                    booking.ActualCheckOutDate = _dateTime.UtcNow;
                }
                else
                    booking.ActualCheckInDate = _dateTime.UtcNow;

                _dbContext.hotelRoomBookings.Update(booking);

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
