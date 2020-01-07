using FluentValidation;

namespace Application.Cars.Queries.GetCarDetail
{
    public class GetCarDetailQueryValidator : AbstractValidator<GetCarDetailQuery>
    {
        public GetCarDetailQueryValidator()
        {
            //RuleFor(v => v.Id).NotEmpty();
        }
    }
}
