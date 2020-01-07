using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Rideshares.Commands.CreateRideshare;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Rideshares.Commands.CreateRideshare
{
    public class CreateRideshareCommandTests : CommandTestBase
    {
        [Fact]
        public async Task HandleCommand_ShouldPersistRideshare()
        {
            var command = new CreateRideshareCommand()
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new CreateRideshareCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Rideshares.Find(result);

            entity.ShouldNotBeNull();
            entity.StartLocation.ShouldBe(command.StartLocation);
            entity.EndLocation.ShouldBe(command.EndLocation);
            entity.StartDate.ShouldBe(command.StartDate);
            entity.EndDate.ShouldBe(command.EndDate);
            entity.Car.Id.ShouldBe(command.Car.Id);
            entity.EmployeeRideshares.Count.ShouldBe(4);

            var employeeRideshares = Context.EmployeeRideshares.Where(er => er.RideshareId == entity.Id).ToList();
            employeeRideshares.ShouldNotBeNull();
            employeeRideshares.Count.ShouldBe(4);
        }

        [Fact]
        public void HandleCommand_ThrowsNotFoundException_ForUnknownCarId()
        {
            var command = new CreateRideshareCommand()
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 999 },
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new CreateRideshareCommandHandler(Context);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
        });
        }

        [Fact]
        public void HandleCommand_ThrowsNotFoundException_ForUnknownEmployeeId()
        {
            var command = new CreateRideshareCommand()
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 999 }
                }
            };

            var handler = new CreateRideshareCommandHandler(Context);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void HandleCommand_ThrowsInvalidDateException_ForStartDateAfterEndDate()
        {
            var command = new CreateRideshareCommand()
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 2, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new CreateRideshareCommandHandler(Context);

            Should.Throw<InvalidDateException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            })
                .Message.ShouldBe(InvalidDateMessageEnum.StartAfterEnd.ToDescriptionString());
        }

        [Fact]
        public void HandleCommand_ThrowsInvalidDateException_ForOccupiedTravelPeriodWithSpecifiedCar()
        {
            var command = new CreateRideshareCommand()
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 2, 16, 0, 0),
                EndDate = new DateTime(2020, 10, 2, 23, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new CreateRideshareCommandHandler(Context);

            Should.Throw<InvalidDateException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            })
                .Message.ShouldBe(InvalidDateMessageEnum.TravelPeriodTaken.ToDescriptionString());
        }
    }
}
