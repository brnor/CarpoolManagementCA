using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmployeeRideshareConfiguration : IEntityTypeConfiguration<EmployeeRideshare>
    {
        public void Configure(EntityTypeBuilder<EmployeeRideshare> builder)
        {
            builder.ToTable("EmployeeRideshares");
            builder.HasKey(er => new { er.EmployeeId, er.RideshareId });

            // TODO: This part might already be implicit and thus unnecessary
            builder
                .HasOne<Employee>(er => er.Employee)
                .WithMany(e => e.EmployeeRideshares)
                .HasForeignKey(er => er.EmployeeId);

            builder
                .HasOne<Rideshare>(er => er.Rideshare)
                .WithMany(e => e.EmployeeRideshares)
                .HasForeignKey(er => er.RideshareId);
        }
    }
}
