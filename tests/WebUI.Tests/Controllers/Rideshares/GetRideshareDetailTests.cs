using Shouldly;
using System.Net;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Rideshares
{
    public class GetRideshareDetailTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetRideshareDetailTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode_ForValidId()
        {
            var client = _factory.GetClient();
            var id = 1;

            var response = await client.GetAsync($"/api/rideshares/{id}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundCode_ForInvalidId()
        {
            var client = _factory.GetClient();
            var id = 999;

            var response = await client.GetAsync($"/api/rideshares/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
