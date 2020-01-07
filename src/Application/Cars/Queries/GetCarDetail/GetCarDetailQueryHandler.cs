using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Queries.GetCarDetail
{
    public class GetCarDetailQueryHandler : IRequestHandler<GetCarDetailQuery, CarDetailVm>
    {
        private readonly ICarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetCarDetailQueryHandler(ICarpoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarDetailVm> Handle(GetCarDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cars
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            return _mapper.Map<CarDetailVm>(entity);
        }
    }
}
