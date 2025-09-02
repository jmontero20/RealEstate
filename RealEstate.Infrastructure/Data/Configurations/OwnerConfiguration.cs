using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;


namespace RealEstate.Infrastructure.Data.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            builder.HasKey(o => o.IdOwner);

            builder.Property(o => o.IdOwner)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.Photo)
                .HasMaxLength(255);

            builder.Property(o => o.Birthday)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            builder.Property(o => o.UpdatedAt);

            builder.Property(o => o.IsDeleted)
                .HasDefaultValue(false);

            // Relationships
            builder.HasMany(o => o.Properties)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.IdOwner)
                .OnDelete(DeleteBehavior.Restrict);

            // Global query filter for soft delete
            builder.HasQueryFilter(o => !o.IsDeleted);
        }
    }
}
