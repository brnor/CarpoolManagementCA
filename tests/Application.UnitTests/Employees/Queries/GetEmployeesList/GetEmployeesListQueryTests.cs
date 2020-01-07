using Application.Employees.Queries.GetEmployeesList;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Employees.Queries.GetEmployeesList
{
    [Collection("QueryTests")]
    public class GetEmployeesListQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectVmAndCount()
        {
            var query = new GetEmployeesListQuery();
            var handler = new GetEmployeesListQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<EmployeesListVm>();
            result.Employees.Count.ShouldBe(5);
        }
    }
}
