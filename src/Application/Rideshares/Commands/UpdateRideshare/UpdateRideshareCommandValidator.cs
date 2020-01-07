using FluentValidation;
using System;
using System.Data;

namespace Application.Rideshares.Commands.UpdateRideshare
{
    public class UpdateRideshareCommandValidator : AbstractValidator<UpdateRideshareCommand>
    {
        public UpdateRideshareCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
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
            RuleFor(x => x.Employees);
            When(x => x.Employees != null, () =>
            {
                RuleFor(x => x.Employees.Count)
                .GreaterThanOrEqualTo(1)
                    .WithMessage("Rideshare requires at least one employee.");
            });
        }
    }

    public class UpdateRideshareCommandSubCarValidator : AbstractValidator<UpdateRideshareCommand.SubCar>
    {
        public UpdateRideshareCommandSubCarValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });
        }
    }

    public class UpdateRideshareCommandSubEmployeeValidator : AbstractValidator<UpdateRideshareCommand.SubEmployee>
    {
        public UpdateRideshareCommandSubEmployeeValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();
            });
        }
    }
}
