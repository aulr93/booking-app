using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class UpdateHotelCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }

        public UpdateHotelCommand(Guid id, string roomNumber, string type, int floor, int price)
        {
            Id = id;
            RoomNumber = roomNumber;
            Type = type;
            Floor = floor;
            Price = price;
        }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMessageLanguageService _messageLanguage;

        public UpdateHotelCommandHandler(IApplicationDbContext dbContext,
            IMessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
          
            try
            {
                var isExist = await _dbContext.HotelRooms.AnyAsync(x => x.Id != request.Id && x.RoomNumber == request.RoomNumber);
                if (isExist)
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.RoomNoIsExist));

                var hotelRoom = await _dbContext.HotelRooms.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (hotelRoom == null)
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.DataNotFound));

                hotelRoom.RoomNumber = request.RoomNumber;
                hotelRoom.Type = request.Type;
                hotelRoom.Floor = request.Floor;
                hotelRoom.Price = request.Price;

                _dbContext.HotelRooms.Update(hotelRoom);

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
