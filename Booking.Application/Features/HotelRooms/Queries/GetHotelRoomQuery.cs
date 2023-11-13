using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Models;
using Booking.Application.Features.HotelRooms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.HotelRooms.Queries
{
    public class GetHotelRoomQuery : BasePagination, IRequest<PaginationResult<HotelRoomVM>>
    {
        public GetHotelRoomQuery(string roomNumber, string type, int floor, int? minPrice, int? maxPrice)
        {
            RoomNumber = roomNumber;
            Type = type;
            Floor = floor;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public string RoomNumber { get; }
        public string Type { get; }
        public int Floor { get; }
        public int? MinPrice { get; }
        public int? MaxPrice { get; }
    }

    public class GetHotelRoomQueryHandler : IRequestHandler<GetHotelRoomQuery, PaginationResult<HotelRoomVM>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetHotelRoomQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResult<HotelRoomVM>> Handle(GetHotelRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.HotelRooms.Where(x => !string.IsNullOrEmpty(request.RoomNumber) ? x.RoomNumber.Contains(request.RoomNumber) : false &&
                                                             !string.IsNullOrEmpty(request.Type) ? x.Type.Contains(request.Type) : false &&
                                                             request.Floor > 0 ? x.Floor == request.Floor : false &&
                                                             request.MinPrice != null ? x.Price >= request.MinPrice : false &&
                                                             request.MaxPrice != null ? x.Price <= request.MaxPrice : false);

                var data = await query.OrderByDescending(x => x.RoomNumber)
                                      .Skip(request.ConvertPageToOffset())
                                      .Take(request.Size)
                                      .Select(x => new HotelRoomVM
                                      {
                                          Id = x.Id,
                                          RoomNumber = x.RoomNumber,
                                          Type = x.Type,
                                          Floor = x.Floor,
                                          Price = x.Price,
                                      })
                                      .ToListAsync();

                return new PaginationResult<HotelRoomVM>
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
