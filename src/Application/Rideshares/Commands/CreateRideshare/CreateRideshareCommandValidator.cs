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
            RuleFor(x => x.Employees)
                .NotEmpty();
            When(x => x.Employees != null, () =>
            {
                RuleFor(x => x.Employees.Count)
                .GreaterThanOrEqualTo(1)
                    .WithMessage("Rideshare requires at least one employee.");
            });
        }
    }

    public class CreateRideshareCommandSubCarValidator : AbstractValidator<CreateRideshareCommand.SubCar>
    {
        public CreateRideshareCommandSubCarValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });
        }
    }

    public class CreateRideshareCommandSubEmployeeValidator : AbstractValidator<CreateRideshareCommand.SubEmployee>
    {
        public CreateRideshareCommandSubEmployeeValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });
        }
    }
}
