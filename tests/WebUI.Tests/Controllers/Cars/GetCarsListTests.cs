using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Cars
{
    public class GetCarsListTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetCarsListTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccesCode()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync($"/api/cars");

            response.EnsureSuccessStatusCode();
        }
    }
}
