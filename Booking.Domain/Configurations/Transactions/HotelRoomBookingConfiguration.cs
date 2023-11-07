using Booking.Domain.Entities;
using Booking.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public abstract class HotelRoomBookingConfiguration : AudittableEntityConfiguration<HotelRoomBooking>
    {
        public override void EntityConfiguration(EntityTypeBuilder<HotelRoomBooking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("trHotelRoomBooking");

            builder.Property(e => e.Id).HasColumnName(name: "Id").ValueGeneratedNever();

            builder.HasOne(x => x.HotelRoom).WithMany(x => x.HotelRoomBookings).HasForeignKey(x => x.RoomId);
        }
    }
}