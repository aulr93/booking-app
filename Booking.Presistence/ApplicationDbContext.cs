using Booking.Application.Commons.Interfaces;
using Booking.Common.Interfaces;
using Booking.Domain;
using Booking.Domain.Configurations;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Masters;
using Booking.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booking.Presistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMachineDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options,
           ICurrentUserService currentUserService,
           IMachineDateTime dateTime
            )
           : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }

        public DbSet<HotelRoomBooking> HotelRoomBookings { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public override DatabaseFacade Database => base.Database;

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified ||
                                e.State == EntityState.Deleted)
                    .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string userId = !string.IsNullOrEmpty(_currentUserService.UserId) ? _currentUserService.UserId.ToString() : DomainConstant.DEFAULT_PERFORMER;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UserIn = !string.IsNullOrEmpty(entry.Entity.UserIn) ? entry.Entity.UserIn : userId;
                        entry.Entity.DateIn = _dateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UserUp = userId;
                        entry.Entity.DateUp = _dateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AdministratorConfiguration().Configure(modelBuilder.Entity<Administrator>());
            new VisitorConfiguration().Configure(modelBuilder.Entity<Visitor>());
            new HotelRoomConfiguration().Configure(modelBuilder.Entity<HotelRoom>());
            new HotelRoomBookingConfiguration().Configure(modelBuilder.Entity<HotelRoomBooking>());

        }
    }
}
