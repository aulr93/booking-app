using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Services;
using Booking.Application.Features.HotelRooms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Queries
{
    public class GetDetailHotelRoomQuery : IRequest<HotelRoomVM>
    {
        public GetDetailHotelRoomQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetDetailHotelRoomQueryHandler : IRequestHandler<GetDetailHotelRoomQuery, HotelRoomVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly MessageLanguageService _messageLanguage;

        public GetDetailHotelRoomQueryHandler(IApplicationDbContext dbContext,
            MessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<HotelRoomVM> Handle(GetDetailHotelRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.HotelRooms.Where(x => x.Id == request.Id)
                                                        .Select(x => new HotelRoomVM
                                                        {
                                                            Id = x.Id,
                                                            RoomNumber = x.RoomNumber,
                                                            Type = x.Type,
                                                            Floor = x.Floor,
                                                            Price = x.Price,
                                                        })
                                                        .FirstOrDefaultAsync();

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
