using Application.Rideshares.Commands.UpdateRideshare;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Rideshares
{
    public class UpdateRideshareTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public UpdateRideshareTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode_ForValidUpdateRideshareCommand()
        {
            var client = _factory.GetClient();
            var id = 1;
            var command = new UpdateRideshareCommand
            {
                Id = id,
                StartLocation = "Zagreb",
                EndLocation = "Bjelovar",
                StartDate = new DateTime(2020, 10, 1, 8, 0, 0),
                EndDate = new DateTime(2020, 10, 1, 14, 0, 0),
                Car = new UpdateRideshareCommand.SubCar { Id = 2 },
                Employees = new List<UpdateRideshareCommand.SubEmployee>()
                {
                    new UpdateRideshareCommand.SubEmployee { Id = 1 },
                    new UpdateRideshareCommand.SubEmployee { Id = 2 },
                    new UpdateRideshareCommand.SubEmployee { Id = 3 }
                }
            };
            var content = TestHelper.GetRequestContent(command);

            var response = await client.PutAsync($"/api/rideshares/{id}", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
