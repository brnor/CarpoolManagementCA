using Shouldly;
using System.Net;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Cars
{
    public class GetCarDetailTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetCarDetailTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccesCode_ForValidId()
        {
            var client = _factory.GetClient();
            var id = 2;

            var response = await client.GetAsync($"/api/cars/{id}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundCode_ForInvalidId()
        {
            var client = _factory.GetClient();
            var id = 999;

            var response = await client.GetAsync($"/api/cars/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
