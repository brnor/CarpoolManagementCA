using Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence
{
    public class CarpoolDataGenerator
    {
        private readonly Dictionary<int, Car> Cars = new Dictionary<int, Car>();
        private readonly Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();

        public static void InitializeDatabase(CarpoolDbContext context)
        {
            var generator = new CarpoolDataGenerator();
            generator.SeedData(context);
        }

        public void SeedData(CarpoolDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cars.Any())
                return;

            SeedCars(context);
            SeedEmployees(context);
            SeedRideshares(context);
        }

        public void SeedCars(CarpoolDbContext context)
        {
            Cars.Add(1,
                new Car
                {
                    Id = 1,
                    Name = "Mercedes GLA Intermediate for longer rides",
                    Type = "Mercedes GLA",
                    Color = "Cosmos Black Metallic",
                    LicensePlate = "ZG-334-CD",
                    NumberOfSeats = 5
                });
            Cars.Add(2,
                new Car
                {
                    Id = 2,
                    Name = "BMW X2 Premium",
                    Type = "BMW X2",
                    Color = "Mineral Grey Metallic",
                    LicensePlate = "ZG-5564-AA",
                    NumberOfSeats = 5
                });
            Cars.Add(3,
                new Car
                {
                    Id = 3,
                    Name = "Mercedes CLA Premium",
                    Type = "Mercedes CLA",
                    Color = "Lunar Blue Metallic",
                    LicensePlate = "ZG-652-FD",
                    NumberOfSeats = 5
                });
            Cars.Add(4,
                new Car
                {
                    Id = 4,
                    Name = "Jaguar XE Executive",
                    Type = "Jaguar XE",
                    Color = "Firenze Red",
                    LicensePlate = "ZG-299-DA",
                    NumberOfSeats = 5
                });
            Cars.Add(5,
                new Car
                {
                    Id = 5,
                    Name = "Audi A4 Luxury",
                    Type = "Audi A4",
                    Color = "Myth Black Metallic",
                    LicensePlate = "ZG-4456-CC",
                    NumberOfSeats = 5
                });
            Cars.Add(6,
                new Car
                {
                    Id = 6,
                    Name = "Ford Transit Standard",
                    Type = "Ford Transit",
                    Color = "Oxford White",
                    LicensePlate = "ZG-941-AD",
                    NumberOfSeats = 10
                });
            Cars.Add(7,
                new Car
                {
                    Id = 7,
                    Name = "Renault Zoe Electric",
                    Type = "Renault Zoe",
                    Color = "Glacier White",
                    LicensePlate = "ZG-1143-BA",
                    NumberOfSeats = 4
                });
            Cars.Add(8,
                new Car
                {
                    Id = 8,
                    Name = "BMW 3 Series Premium",
                    Type = "BMW 320d SE",
                    Color = "Mineral Grey",
                    LicensePlate = "ZG-555-BB",
                    NumberOfSeats = 5
                });
            Cars.Add(9,
                new Car
                {
                    Id = 9,
                    Name = "Citroen C3",
                    Type = "Citroen C3 Aircross",
                    Color = "Cobalt Blue",
                    LicensePlate = "ZG-5663-AA",
                    NumberOfSeats = 5
                });
            Cars.Add(10,
                new Car
                {
                    Id = 10,
                    Name = "Skoda Fabia Urban",
                    Type = "Skoda Fabia Hatch",
                    Color = "Briliant Silver",
                    LicensePlate = "ZG-778-DC",
                    NumberOfSeats = 5
                });

            context.Cars.AddRange(Cars.Values.ToList());

            context.SaveChanges();
        }

        public void SeedEmployees(CarpoolDbContext context)
        {
            Employees.Add(1,
                new Employee
                {
                    Id = 1,
                    FirstName = "Marko",
                    LastName = "Kranjcec",
                    IsDriver = true
                });
            Employees.Add(2,
                new Employee
                {
                    Id = 2,
                    FirstName = "Karlo",
                    LastName = "Maletic",
                    IsDriver = false
                });
            Employees.Add(3,
                new Employee
                {
                    Id = 3,
                    FirstName = "Matija",
                    LastName = "Cesar",
                    IsDriver = true
                });
            Employees.Add(4,
                new Employee
                {
                    Id = 4,
                    FirstName = "Luka",
                    LastName = "Blazic",
                    IsDriver = false
                });
            Employees.Add(5,
                new Employee
                {
                    Id = 5,
                    FirstName = "Katarina",
                    LastName = "Lipovac",
                    IsDriver = true
                });
            Employees.Add(6,
                new Employee
                {
                    Id = 6,
                    FirstName = "Anita",
                    LastName = "Jandras",
                    IsDriver = true
                });
            Employees.Add(7,
                new Employee
                {
                    Id = 7,
                    FirstName = "Borna",
                    LastName = "Horvat",
                    IsDriver = false
                });
            Employees.Add(8,
                new Employee
                {
                    Id = 8,
                    FirstName = "Martina",
                    LastName = "Peic",
                    IsDriver = false
                });
            Employees.Add(9,
                new Employee
                {
                    Id = 9,
                    FirstName = "Laura",
                    LastName = "Horvat",
                    IsDriver = false
                });
            Employees.Add(10,
                new Employee
                {
                    Id = 10,
                    FirstName = "Bozidar",
                    LastName = "Gaspar",
                    IsDriver = true
                });
            Employees.Add(11,
                new Employee
                {
                    Id = 11,
                    FirstName = "Andrija",
                    LastName = "Dominikovic",
                    IsDriver = true
                });
            Employees.Add(12,
                new Employee
                {
                    Id = 12,
                    FirstName = "Bruno",
                    LastName = "Kovac",
                    IsDriver = false
                });
            Employees.Add(13,
                new Employee
                {
                    Id = 13,
                    FirstName = "Vesna",
                    LastName = "Dudas",
                    IsDriver = true
                });
            Employees.Add(14,
                new Employee
                {
                    Id = 14,
                    FirstName = "Marina",
                    LastName = "Poljak",
                    IsDriver = false
                });
            Employees.Add(15, 
                new Employee
                {
                    Id = 15,
                    FirstName = "Nada",
                    LastName = "Medved",
                    IsDriver = true
                });
            Employees.Add(16,
                new Employee
                {
                    Id = 16,
                    FirstName = "Kristina",
                    LastName = "Madunic",
                    IsDriver = true
                });
            Employees.Add(17,
                new Employee
                {
                    Id = 17,
                    FirstName = "Mirko",
                    LastName = "Nemet",
                    IsDriver = false
                });
            Employees.Add(18,
                new Employee
                {
                    Id = 18,
                    FirstName = "Vedran",
                    LastName = "Zubak",
                    IsDriver = false
                });
            Employees.Add(19,
                new Employee
                {
                    Id = 19,
                    FirstName = "Patrik",
                    LastName = "Colic",
                    IsDriver = false
                });
            Employees.Add(20,
                new Employee
                {
                    Id = 20,
                    FirstName = "Budimir",
                    LastName = "Belic",
                    IsDriver = false
                });
            Employees.Add(21,
                new Employee
                {
                    Id = 21,
                    FirstName = "Tamara",
                    LastName = "Cigir",
                    IsDriver = false
                });
            Employees.Add(22,
                new Employee
                {
                    Id = 22,
                    FirstName = "Ljiljana",
                    LastName = "Vidovic",
                    IsDriver = true
                });
            Employees.Add(23,
                new Employee
                {
                    Id = 23,
                    FirstName = "Mia",
                    LastName = "Srsen",
                    IsDriver = true
                });
            Employees.Add(24,
                new Employee
                {
                    Id = 24,
                    FirstName = "Frodo",
                    LastName = "Baggins",
                    IsDriver = false
                });
            Employees.Add(25,
                new Employee
                {
                    Id = 25,
                    FirstName = "Samwise",
                    LastName = "Gamgee",
                    IsDriver = true
                });

            context.Employees.AddRange(Employees.Values.ToList());

            context.SaveChanges();
        }

        public void SeedRideshares(CarpoolDbContext context)
        {
            var rideshares = new[]
            {
                new Rideshare
                {
                    Id = 1,
                    Car = Cars[1],
                    StartLocation = "Zagreb",
                    EndLocation = "Rijeka",
                    StartDate = new DateTime(2020, 1, 4, 8, 0, 0),
                    EndDate = new DateTime(2020, 1, 4, 18, 30, 0),
                    EmployeeRideshares = new[]
                    {
                        new EmployeeRideshare { EmployeeId = 4 },
                        new EmployeeRideshare { EmployeeId = 10 },
                        new EmployeeRideshare { EmployeeId = 19 },
                        new EmployeeRideshare { EmployeeId = 6 }
                    }
                },
                new Rideshare
                {
                    Id = 2,
                    Car = Cars[4],
                    StartLocation = "Zagreb",
                    EndLocation = "Split",
                    StartDate = new DateTime(2020, 1, 7, 8, 0, 0),
                    EndDate = new DateTime(2020, 1, 7, 17, 0, 0),
                    EmployeeRideshares = new[]
                    {
                        new EmployeeRideshare { EmployeeId = 5 },
                        new EmployeeRideshare { EmployeeId = 12 },
                        new EmployeeRideshare { EmployeeId = 20 }
                    }
                }
            };

            context.Rideshares.AddRange(rideshares.ToList());

            context.SaveChanges();
        }
    }
}
