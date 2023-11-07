using Booking.Domain.Entities;
using Booking.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booking.Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        public DatabaseFacade Database { get; }

        public DbSet<HotelRoom> hotelRooms { get; set; }

        public DbSet<HotelRoomBooking> hotelRoomBookings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        void DetachAllEntities();
    }
}
