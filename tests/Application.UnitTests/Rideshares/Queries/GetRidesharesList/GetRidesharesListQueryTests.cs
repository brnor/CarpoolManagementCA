using Application.Rideshares.Queries.GetRidesharesList;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Rideshares.Queries.GetRidesharesList
{
    [Collection("QueryTests")]
    public class GetRidesharesListQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetRidesharesListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectVmAndCount()
        {
            var query = new GetRidesharesListQuery();
            var handler = new GetRidesharesListQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<RidesharesListVm>();
            result.Rideshares.Count.ShouldBe(2);

            var rideshare = result.Rideshares.First();

            rideshare.StartLocation.ShouldBe("Zagreb");
            rideshare.EndLocation.ShouldBe("Split");
        }
    }
}
