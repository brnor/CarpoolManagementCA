using Application.Rideshares.Commands.CreateRideshare;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Rideshares
{
    public class CreateRideshareTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public CreateRideshareTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode_ForValidCreateRideshareCommand()
        {
            var client = _factory.GetClient();
            var command = new CreateRideshareCommand
            {
                StartLocation = "Zagreb",
                EndLocation = "Osijek",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new CreateRideshareCommand.SubCar { Id = 1},
                Employees = new List<CreateRideshareCommand.SubEmployee>()
                {
                    new CreateRideshareCommand.SubEmployee { Id = 1 },
                    new CreateRideshareCommand.SubEmployee { Id = 2 },
                    new CreateRideshareCommand.SubEmployee { Id = 3 },
                    new CreateRideshareCommand.SubEmployee { Id = 4 }
                }
            };
            var content = TestHelper.GetRequestContent(command);

            var response = await client.PostAsync($"/api/rideshares", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
