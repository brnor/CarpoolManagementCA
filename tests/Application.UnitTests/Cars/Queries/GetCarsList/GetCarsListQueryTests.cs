using Application.Cars.Queries.GetCarsList;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Cars.Queries.GetCarsList
{
    [Collection("QueryTests")]
    public class GetCarsListQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetCarsListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectVmAndCount()
        {
            var query = new GetCarsListQuery();
            var handler = new GetCarsListQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<CarsListVm>();
            result.Cars.Count.ShouldBe(2);

            var car = result.Cars.First();

            car.Type.ShouldBe("Skoda Octavia");
        }
    }
}
