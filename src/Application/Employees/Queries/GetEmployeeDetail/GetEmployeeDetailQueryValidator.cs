using FluentValidation;

namespace Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQueryValidator : AbstractValidator<GetEmployeeDetailQuery>
    {
        public GetEmployeeDetailQueryValidator()
        {
            //RuleFor(v => v.Id).NotEmpty();
        }
    }
}
