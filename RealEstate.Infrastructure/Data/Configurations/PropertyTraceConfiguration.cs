using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations
{
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.ToTable("PropertyTraces");

            builder.HasKey(pt => pt.IdPropertyTrace);

            builder.Property(pt => pt.IdPropertyTrace)
                .ValueGeneratedOnAdd();

            builder.Property(pt => pt.DateSale)
                .IsRequired();

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pt => pt.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pt => pt.Tax)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pt => pt.IdProperty)
                .IsRequired();

            builder.Property(pt => pt.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            builder.Property(pt => pt.UpdatedAt);

            builder.Property(pt => pt.IsDeleted)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(pt => pt.Property)
                .WithMany(p => p.PropertyTraces)
                .HasForeignKey(pt => pt.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            // Global query filter for soft delete
            builder.HasQueryFilter(pt => !pt.IsDeleted);
        }
    }
}
