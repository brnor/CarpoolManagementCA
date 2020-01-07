using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Rideshares
{
    public class GetRidesharesListTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetRidesharesListTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync("/api/rideshares");

            response.EnsureSuccessStatusCode();
        }
    }
}
