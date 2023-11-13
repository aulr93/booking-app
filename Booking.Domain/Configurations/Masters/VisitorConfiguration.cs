using Booking.Domain.Constant;
using Booking.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public class VisitorConfiguration : AudittableEntityConfiguration<Visitor>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Visitor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(AppTable.Visitor);

            builder.Property(e => e.Id).HasColumnName(name: "Id").ValueGeneratedNever();
        }
    }
}