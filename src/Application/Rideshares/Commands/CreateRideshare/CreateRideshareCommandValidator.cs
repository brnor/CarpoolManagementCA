using FluentValidation;
using System;

namespace Application.Rideshares.Commands.CreateRideshare
{
    public class CreateRideshareCommandValidator : AbstractValidator<CreateRideshareCommand>
    {
        public CreateRideshareCommandValidator()
        {
            RuleFor(x => x.StartLocation)
                .NotEmpty();
            RuleFor(x => x.EndLocation)
                .NotEmpty();
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("Start date can not be set in the past.");
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now)
                    .WithMessage("End date can not be set in the past.");
            RuleFor(x => x.Car)
                .NotEmpty();
            RuleFor(x => x.Car.Id)
                .NotEmpty()
                .When(x => x.Car != null);
            RuleFor(x => x.Employees)
                .NotEmpty()
                    .WithMessage("Rideshare requires at least one employee.");
            RuleForEach(x => x.Employees)
                .SetValidator(new CreateRideshareCommandSubEmployeeValidator());
        }
    }

    public class CreateRideshareCommandSubEmployeeValidator : AbstractValidator<CreateRideshareCommand.SubEmployee>
    {
        public CreateRideshareCommandSubEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
