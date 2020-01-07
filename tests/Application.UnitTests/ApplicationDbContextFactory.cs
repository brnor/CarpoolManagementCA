using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests
{
    public static class ApplicationDbContextFactory
    {
        public static CarpoolDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CarpoolDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CarpoolDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(CarpoolDbContext context)
        {
            var cars = new List<Car> 
            {
                new Car
                {
                    Id = 1,
                    Name = "Skoda for longer trips",
                    Type = "Skoda Octavia",
                    Color = "Silver",
                    LicensePlate = "ZG-2353-C",
                    NumberOfSeats = 5
                },
                new Car
                {
                    Id = 2,
                    Name = "Mazda for VIP",
                    Type = "Mazda 3",
                    Color = "Jet Black Mica",
                    LicensePlate = "ZG-8028-CC",
                    NumberOfSeats = 4
                }
            };

            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "Luka",
                    LastName = "Savic",
                    IsDriver = true
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Antonio",
                    LastName = "Vrancic",
                    IsDriver = false
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Magdalena",
                    LastName = "Vrabec",
                    IsDriver = true
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Ana",
                    LastName = "Plesa",
                    IsDriver = false
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Natasa",
                    LastName = "Turk",
                    IsDriver = false
                }
            };

            var rideshares = new List<Rideshare>
            {
                new Rideshare
                {
                    Id = 1,
                    StartLocation = "Zagreb",
                    EndLocation = "Split",
                    StartDate = new DateTime(2020, 10, 2, 12, 0, 0),
                    EndDate = new DateTime(2020, 10, 3, 9, 0, 0),
                    Car = cars[0]
                },
                new Rideshare
                {
                    Id = 2,
                    StartLocation = "Zagreb",
                    EndLocation = "Dubrovnik",
                    StartDate = new DateTime(2020, 11, 10, 8, 0, 0),
                    EndDate = new DateTime(2020, 11, 10, 19, 0, 0),
                    Car = cars[1]
                }
            };

            var employeeRideshares = new List<EmployeeRideshare>();
            for (int i = 0; i <= 4; ++i)
            {
                employeeRideshares.Add(
                    new EmployeeRideshare
                    {
                        Employee = employees[i],
                        EmployeeId = employees[i].Id,
                        Rideshare = rideshares[0],
                        RideshareId = rideshares[0].Id
                    }
                );
                employeeRideshares.Add(
                    new EmployeeRideshare
                    {
                        Employee = employees[i],
                        EmployeeId = employees[i].Id,
                        Rideshare = rideshares[1],
                        RideshareId = rideshares[1].Id
                    }
                );
            }

            foreach(var r in rideshares)
            {
                foreach(var er in employeeRideshares)
                {
                    if (r.Id == er.RideshareId)
                    {
                        r.EmployeeRideshares.Add(er);
                    }
                }
            }
            
            context.Cars.AddRange(cars);
            context.Employees.AddRange(employees);
            context.EmployeeRideshares.AddRange(employeeRideshares);
            context.Rideshares.AddRange(rideshares);

            context.SaveChanges();
        }

        public static void Destroy(CarpoolDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
