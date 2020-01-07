﻿using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Tests.Common
{
    public static class TestDataGenerator
    {
        public static void SeedSampleData(CarpoolDbContext context)
        {
            if (context.Cars.Any())
                return;
            SeedCarSampleData(context);
            SeedEmployeeSampleData(context);
            SeedRideshareSampleData(context);

            //context.SaveChanges();
        }

        private static void SeedCarSampleData(CarpoolDbContext context)
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

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void SeedEmployeeSampleData(CarpoolDbContext context)
        {
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

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private static void SeedRideshareSampleData(CarpoolDbContext context)
        {
            var cars = context.Cars.ToList();
            var employees = context.Employees.ToList();

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
            }

            employeeRideshares.ForEach(er => rideshares[0].EmployeeRideshares.Add(er));

            context.EmployeeRideshares.AddRange(employeeRideshares);
            context.Rideshares.AddRange(rideshares);

            context.SaveChanges();
        }
    }
}
