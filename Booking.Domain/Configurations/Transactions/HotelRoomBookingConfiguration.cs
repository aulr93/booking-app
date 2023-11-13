using Booking.Domain.Constant;
using Booking.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public class HotelRoomBookingConfiguration : AudittableEntityConfiguration<HotelRoomBooking>
    {
        public override void EntityConfiguration(EntityTypeBuilder<HotelRoomBooking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(AppTable.HotelRoomBooking);

            builder.Property(e => e.Id).HasColumnName(name: "Id").ValueGeneratedNever();

            builder.HasOne(x => x.HotelRoom).WithMany(x => x.HotelRoomBookings).HasForeignKey(x => x.RoomId);
            builder.HasOne(x => x.Visitor).WithMany(x => x.HotelRoomBookings).HasForeignKey(x => x.VisitorId);
        }
    }
}