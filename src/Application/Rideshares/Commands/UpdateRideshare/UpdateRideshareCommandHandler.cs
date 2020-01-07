using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rideshares.Commands.UpdateRideshare
{
    public class UpdateRideshareCommandHandler : IRequestHandler<UpdateRideshareCommand>
    {
        private readonly ICarpoolDbContext _context;

        public UpdateRideshareCommandHandler(ICarpoolDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRideshareCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rideshares
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Rideshare), request.Id);
            }

            // TODO: Split into multiple smaller methods
            // TODO: optimize this to skip checking unchanged fields
            // Date validation logic
            if (request.StartDate.Value >= request.EndDate)
            {
                throw new InvalidDateException(InvalidDateMessageEnum.StartAfterEnd);
            }

            // Check if selected car exists
            var car = await _context.Cars
                .FindAsync(request.Car.Id);

            if (car == null)
            {
                throw new NotFoundException(nameof(Car), request.Car.Id);
            }

            // Check if specified employees exist
            foreach (var e in request.Employees)
            {
                var employee = await _context.Employees
                    .FindAsync(e.Id);
                if (employee == null)
                {
                    throw new NotFoundException(nameof(Employee), e.Id);
                }
            }

            // Check if car is available during selected time period
            var rideshares = await _context.Rideshares
                .Include(r => r.Car)
                .Where(r => r.Car.Id == request.Car.Id)
                .Where(r => r.EndDate > request.StartDate)
                .Where(r => r.Id != request.Id) // since we want to update this rideshare we skip checking on it
                .ToListAsync();
            foreach (var r in rideshares)
            {
                if (request.StartDate < r.StartDate)
                {
                    if (request.EndDate > r.StartDate)
                    {
                        throw new InvalidDateException(InvalidDateMessageEnum.TravelPeriodTaken);
                    }
                }
                else
                {
                    if (request.StartDate < r.EndDate)
                    {
                        throw new InvalidDateException(InvalidDateMessageEnum.TravelPeriodTaken);
                    }
                }
            }

            // Check if employees can fit into select car
            var employees = await _context.Employees
                .Select(ce => new { ce.Id, ce.IsDriver })
                .Where(ce => request.Employees.Select(re => re.Id).Contains(ce.Id))
                .ToListAsync();
            if (employees.Count > car.NumberOfSeats)
            {
                throw new TooManyEmployeesException(request.Car.Id);
            }

            // Check if at least one employee is a driver
            if (!employees.Any(e => e.IsDriver))
            {
                throw new NoDriverException();
            }

            entity.StartLocation = request.StartLocation;
            entity.EndLocation = request.EndLocation;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Car = car;

            // Remove old employee-rideshare entities and remake them with new IDs
            var erForRemoval = await _context.EmployeeRideshares
                .Where(er => er.RideshareId == request.Id)
                .ToListAsync();
            _context.EmployeeRideshares.RemoveRange(erForRemoval);

            entity.EmployeeRideshares.Clear();

            foreach (var e in request.Employees)
            {
                var er = new EmployeeRideshare
                {
                    Employee = await _context.Employees.FindAsync(e.Id),
                    EmployeeId = e.Id,
                    Rideshare = entity,
                    RideshareId = entity.Id
                };
                entity.EmployeeRideshares.Add(er);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
