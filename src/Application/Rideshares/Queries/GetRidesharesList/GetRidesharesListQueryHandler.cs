using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rideshares.Queries.GetRidesharesList
{
    public class GetRidesharesListQueryHandler : IRequestHandler<GetRidesharesListQuery, RidesharesListVm>
    {
        private readonly ICarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetRidesharesListQueryHandler(ICarpoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RidesharesListVm> Handle(GetRidesharesListQuery request, CancellationToken cancellationToken)
        {
            var rideshares = await _context.Rideshares
                .ProjectTo<RideshareDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var vm = new RidesharesListVm
            {
                Rideshares = rideshares
            };

            return vm;
        }
    }
}
