using Booking.Domain.Constant;
using Booking.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public class AdministratorConfiguration : AudittableEntityConfiguration<Administrator>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(AppTable.Administrator);

            builder.Property(e => e.Id).HasColumnName(name: "Id").ValueGeneratedNever();
        }
    }
}