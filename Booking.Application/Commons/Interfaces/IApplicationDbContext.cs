using Booking.Domain.Entities;
using Booking.Domain.Entities.Masters;
using Booking.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booking.Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        public DatabaseFacade Database { get; }

        public DbSet<HotelRoom> HotelRooms { get; set; }

        public DbSet<HotelRoomBooking> HotelRoomBookings { get; set; }
        
        public DbSet<Administrator> Administrators { get; set; }
        
        public DbSet<Visitor> Visitors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        void DetachAllEntities();
    }
}
