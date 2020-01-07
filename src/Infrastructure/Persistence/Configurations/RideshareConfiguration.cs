using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RideshareConfiguration : IEntityTypeConfiguration<Rideshare>
    {
        public void Configure(EntityTypeBuilder<Rideshare> builder)
        {
            builder.ToTable("Rideshares");
            builder.HasKey("Id");

            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();
            builder.Property(r => r.StartLocation)
                .HasMaxLength(35);
            builder.Property(r => r.EndLocation)
                .HasMaxLength(35);
            builder.Property(r => r.StartDate)
                .HasColumnType("datetime");
            builder.Property(r => r.EndDate)
                .HasColumnType("datetime");

            builder.HasOne(r => r.Car)
                .WithMany(c => c.Rideshares)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
