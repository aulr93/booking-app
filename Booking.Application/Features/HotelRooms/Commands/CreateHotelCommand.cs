using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Resources;
using Booking.Application.Commons.Services;
using Booking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class CreateHotelCommand : IRequest<Unit>
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }

        public CreateHotelCommand(string roomNumber, string type, int floor, int price)
        {
            RoomNumber = roomNumber;
            Type = type;
            Floor = floor;
            Price = price;
        }
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly MessageLanguageService _messageLanguage;

        public CreateHotelCommandHandler(IApplicationDbContext dbContext,
            MessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var isExist = await _dbContext.HotelRooms.AnyAsync(x => x.RoomNumber == request.RoomNumber);
                if (isExist)
                    throw new BadRequestException(_messageLanguage[MessageCodeConstant.DataExist]);

                _dbContext.HotelRooms.Add(new HotelRoom
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = request.RoomNumber,
                    Type = request.Type,
                    Floor = request.Floor,
                    Price = request.Price,
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
