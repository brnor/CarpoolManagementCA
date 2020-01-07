using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");
            builder.HasKey("Id");

            builder.HasIndex(x => x.LicensePlate)
                .IsUnique();

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Name)
                .HasMaxLength(255);
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(c => c.Color)
                .HasMaxLength(32);
            builder.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(c => c.NumberOfSeats)
                .IsRequired();
        }
    }
}
