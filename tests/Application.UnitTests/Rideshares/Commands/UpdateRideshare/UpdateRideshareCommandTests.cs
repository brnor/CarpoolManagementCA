using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Rideshares.Commands.UpdateRideshare;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Rideshares.Commands.UpdateRideshare
{
    public class UpdateRideshareCommandTests : CommandTestBase
    {
        [Fact]
        public async Task HandleCommand_ShouldPersistRideshareUpdate()
        {
            var id = 1;
            var command = new UpdateRideshareCommand()
            {
                Id = id,
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2021, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2021, 10, 1, 14, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 },
                    new UpdateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new UpdateRideshareCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Rideshares.Find(id);

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
            var id = 1;
            var command = new UpdateRideshareCommand()
            {
                Id = 1,
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 999 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 },
                    new UpdateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new UpdateRideshareCommandHandler(Context);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void HandleCommand_ThrowsNotFoundException_ForUnknownEmployeeId()
        {
            var id = 1;
            var command = new UpdateRideshareCommand()
            {
                Id = id,
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 },
                    new UpdateRideshareCommand.SubEmployee { Id = 999 }
                }
            };

            var handler = new UpdateRideshareCommandHandler(Context);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void HandleCommand_ThrowsInvalidDateException_ForStartDateAfterEndDate()
        {
            var id = 1;
            var command = new UpdateRideshareCommand()
            {
                Id = 1,
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 2, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 1 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 },
                    new UpdateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new UpdateRideshareCommandHandler(Context);

            Should.Throw<InvalidDateException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            })
                .Message.ShouldBe(InvalidDateMessageEnum.StartAfterEnd.ToDescriptionString());
        }

        [Fact]
        public void HandleCommand_ThrowsInvalidDateException_ForOccupiedTravelPeriodWithSpecifiedCar()
        {
            var id = 1;
            var command = new UpdateRideshareCommand()
            {
                Id = id,
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 11, 10, 10, 0, 0),
                EndDate = new DateTime(2020, 11, 10, 15, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 2 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 },
                    new UpdateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var handler = new UpdateRideshareCommandHandler(Context);

            Should.Throw<InvalidDateException>(async () =>
            {
                var result = await handler.Handle(command, CancellationToken.None);
            })
                .Message.ShouldBe(InvalidDateMessageEnum.TravelPeriodTaken.ToDescriptionString());
        }
    }
}
