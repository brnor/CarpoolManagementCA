using Application.Common.Exceptions;
using Application.Rideshares.Queries.GetRideshareDetail;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Rideshares.Queries.GetRideshareDetail
{
    [Collection("QueryTests")]
    public class GetRideshareDetailQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetRideshareDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectVmAndDataForExistingId()
        {
            var query = new GetRideshareDetailQuery
            {
                Id = 1
            };
            var handler = new GetRideshareDetailQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<RideshareDetailVm>();
            result.StartLocation.ShouldBe("Zagreb");
            result.EndLocation.ShouldBe("Split");
            result.StartDate.ShouldBe(new DateTime(2020, 10, 2, 12, 0, 0));
            result.EndDate.ShouldBe(new DateTime(2020, 10, 3, 9, 0, 0));
            result.Car.Type.ShouldBe("Skoda Octavia");
            result.Employees.Count.ShouldBe(5);
        }

        [Fact]
        public void HandleQuery_ThrowsNotFoundExceptionForUnknownId()
        {
            var query = new GetRideshareDetailQuery
            {
                Id = 999
            };
            var handler = new GetRideshareDetailQueryHandler(_context, _mapper);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
