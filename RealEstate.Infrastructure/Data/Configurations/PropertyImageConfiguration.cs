using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");

            builder.HasKey(pi => pi.IdPropertyImage);

            builder.Property(pi => pi.IdPropertyImage)
                .ValueGeneratedOnAdd();

            builder.Property(pi => pi.IdProperty)
                .IsRequired();

            builder.Property(pi => pi.File)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pi => pi.Enabled)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(pi => pi.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            builder.Property(pi => pi.UpdatedAt);

            builder.Property(pi => pi.IsDeleted)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(pi => pi.Property)
                .WithMany(p => p.PropertyImages)
                .HasForeignKey(pi => pi.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            // Global query filter for soft delete
            builder.HasQueryFilter(pi => !pi.IsDeleted);
        }
    }
}
