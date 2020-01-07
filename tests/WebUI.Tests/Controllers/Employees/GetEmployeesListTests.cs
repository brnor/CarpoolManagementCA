using System.Threading.Tasks;
using WebUI.Tests.Common;
using Xunit;

namespace WebUI.Tests.Controllers.Employees
{
    public class GetEmployeesListTests : IClassFixture<CustomWebAppFactory<Startup>>
    {
        private readonly CustomWebAppFactory<Startup> _factory;

        public GetEmployeesListTests(CustomWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessCode()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync($"/api/employees");

            response.EnsureSuccessStatusCode();
        }
    }
}
