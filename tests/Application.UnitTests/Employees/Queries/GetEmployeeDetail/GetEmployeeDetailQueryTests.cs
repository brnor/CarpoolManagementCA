using Application.Common.Exceptions;
using Application.Employees.Queries.GetEmployeeDetail;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Employees.Queries.GetEmployeeDetail
{
    [Collection("QueryTests")]
    public class GetEmployeeDetailQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectVmAndData()
        {
            var query = new GetEmployeeDetailQuery
            {
                Id = 3
            };
            var handler = new GetEmployeeDetailQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<EmployeeDetailVm>();
            result.FirstName.ShouldBe("Magdalena");
            result.LastName.ShouldBe("Vrabec");
            result.IsDriver.ShouldBe(true);
        }

        [Fact]
        public void HandleQuery_ThrowsNotFoundExceptionForUnknownId()
        {
            var query = new GetEmployeeDetailQuery
            {
                Id = 999
            };
            var handler = new GetEmployeeDetailQueryHandler(_context, _mapper);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
