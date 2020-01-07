using FluentValidation;

namespace Application.Rideshares.Commands.DeleteRideshare
{
    public class DeleteRideshareCommandValidator : AbstractValidator<DeleteRideshareCommand>
    {
        public DeleteRideshareCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
