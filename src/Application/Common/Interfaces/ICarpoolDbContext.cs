using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICarpoolDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Rideshare> Rideshares { get; set; }
        DbSet<EmployeeRideshare> EmployeeRideshares { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
