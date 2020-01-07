using Shouldly;
using System.Net;
using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Employees
{
    public class GetEmployeeDetailTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetEmployeeDetailTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnSuccessCode_ForValidId()
        {
            var client = _factory.GetClient();
            var id = 1;

            var response = await client.GetAsync($"/api/employees/{id}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsNotFoundCode_ForInvalidId()
        {
            var client = _factory.GetClient();
            var id = 999;

            var response = await client.GetAsync($"/api/employees/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
