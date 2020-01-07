using Application.Cars.Queries.GetCarDetail;
using Application.Common.Exceptions;
using Application.UnitTests.Common;
using AutoMapper;
using Infrastructure.Persistence;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Cars.Queries.GetCarDetail
{
    [Collection("QueryTests")]
    public class GetCarDetailQueryTests
    {
        private readonly CarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetCarDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task HandleQuery_ReturnCorrectVmAndData()
        {
            var query = new GetCarDetailQuery
            {
                Id = 1
            };
            var handler = new GetCarDetailQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<CarDetailVm>();

            result.Type.ShouldBe("Skoda Octavia");
            result.Color.ShouldBe("Silver");
            result.NumberOfSeats.ShouldBe(5);
        }

        [Fact]
        public void HandleQuery_ThrowsNotFoundExceptionForUnknownId()
        {
            var query = new GetCarDetailQuery
            {
                Id = 99
            };
            var handler = new GetCarDetailQueryHandler(_context, _mapper);

            Should.Throw<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}

