using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Queries.GetCarsList
{
    public class GetCarsListQueryHandler : IRequestHandler<GetCarsListQuery, CarsListVm>
    {
        private readonly ICarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetCarsListQueryHandler(ICarpoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarsListVm> Handle(GetCarsListQuery request, CancellationToken cancellationToken)
        {
            var cars = await _context.Cars
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CarsListVm
            {
                Cars = cars
            };

            return vm;
        }
    }
}
