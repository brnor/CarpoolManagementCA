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

namespace Application.Rideshares.Commands.CreateRideshare
{
    public class CreateRideshareCommandHandler : IRequestHandler<CreateRideshareCommand, int>
    {
        private readonly ICarpoolDbContext _context;

        public CreateRideshareCommandHandler(ICarpoolDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRideshareCommand request, CancellationToken cancellationToken)
        {
            // TODO: Split into multiple smaller methods
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
            foreach (var e  in request.Employees)
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
                .ToListAsync();
            foreach(var r in rideshares)
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

            // Create new Rideshare entity
            var entity = new Rideshare
            {
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Car = car
            };

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
            _context.Rideshares.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
