using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(p => p.IdProperty);

            builder.Property(p => p.IdProperty)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CodeInternal)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Year)
                .IsRequired();

            builder.Property(p => p.IdOwner)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            builder.Property(p => p.UpdatedAt);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            // Indexes
            builder.HasIndex(p => p.CodeInternal)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Price);
            builder.HasIndex(p => p.Year);

            // Relationships
            builder.HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.IdOwner)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PropertyImages)
                .WithOne(pi => pi.Property)
                .HasForeignKey(pi => pi.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PropertyTraces)
                .WithOne(pt => pt.Property)
                .HasForeignKey(pt => pt.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            // Global query filter for soft delete
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
