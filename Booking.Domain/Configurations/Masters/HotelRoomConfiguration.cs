using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public abstract class HotelRoomConfiguration : AudittableEntityConfiguration<HotelRoom>
    {
        public override void EntityConfiguration(EntityTypeBuilder<HotelRoom> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("msHotelRoom");

            builder.Property(e => e.Id).HasColumnName(name: "Id").ValueGeneratedNever();

            builder.Property(e => e.RoomNumber).HasMaxLength(maxLength: 5);
        }
    }
}