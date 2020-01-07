using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey("Id");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(35);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(35);
            builder.Property(e => e.IsDriver)
                .IsRequired()
                .HasColumnType("int")
                .HasDefaultValue(false);
        }
    }
}
