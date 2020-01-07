using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rideshares.Commands.DeleteRideshare
{
    public class DeleteRideshareCommandHandler : IRequestHandler<DeleteRideshareCommand>
    {
        private readonly ICarpoolDbContext _context;

        public DeleteRideshareCommandHandler(ICarpoolDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRideshareCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rideshares
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Rideshare), request.Id);
            }

            // Don't rely on cascade, manually remove all EmployeeRideshare entities
            var employeeRideshares = await _context.EmployeeRideshares
                .Where(er => er.RideshareId == request.Id)
                .ToListAsync();

            _context.EmployeeRideshares.RemoveRange(employeeRideshares);

            _context.Rideshares.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
