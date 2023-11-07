using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Configurations
{
    public abstract class AudittableEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : AuditableEntity
    {
        public void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.Property(p => p.UserIn).HasMaxLength(maxLength: 100);

            builder.HasIndex(p => p.UserIn);

            builder.HasIndex(p => p.DateIn);

            builder.Property(p => p.UserUp).HasMaxLength(maxLength: 100);

            builder.HasIndex(p => p.UserUp);

            builder.HasIndex(p => p.DateUp);

            EntityConfiguration(builder);
        }

        public abstract void EntityConfiguration(EntityTypeBuilder<TBaseEntity> builder);
    }
}