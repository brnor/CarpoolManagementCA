using Shouldly;
using System.Net;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Rideshares
{
    public class DeleteRideshareTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public DeleteRideshareTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode_ForValidId()
        {
            var id = 1;
            var client = _factory.GetClient();

            var response = await client.DeleteAsync($"/api/rideshares/{id}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundCode_ForInvalidId()
        {
            var id = 999;
            var client = _factory.GetClient();

            var response = await client.DeleteAsync($"/api/rideshares/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
