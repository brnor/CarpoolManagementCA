using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rideshares.Queries.GetRideshareDetail
{
    public class GetRideshareDetailQueryHandler : IRequestHandler<GetRideshareDetailQuery, RideshareDetailVm>
    {
        private readonly ICarpoolDbContext _context;
        private readonly IMapper _mapper;

        public GetRideshareDetailQueryHandler(ICarpoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RideshareDetailVm> Handle(GetRideshareDetailQuery request, CancellationToken cancellationToken)
        {
            var rideshare = await _context.Rideshares
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (rideshare == null)
            {
                throw new NotFoundException(nameof(Rideshare), request.Id);
            }
            
            var vm = _mapper.Map<RideshareDetailVm>(rideshare);

            var employeeRideshareIds = await _context.EmployeeRideshares
                .Where(er => er.RideshareId.Equals(request.Id))
                .Select(er => er.EmployeeId)
                .ToListAsync();
            
            vm.Employees = await _context.Employees
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .Where(e => employeeRideshareIds.Contains(e.Id)) // TOOD: Add error handling for this?
                .ToListAsync();

            return vm;
        }
    }
}
