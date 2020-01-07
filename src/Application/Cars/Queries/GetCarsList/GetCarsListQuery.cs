using MediatR;

namespace Application.Cars.Queries.GetCarsList
{
    public class GetCarsListQuery : IRequest<CarsListVm>
    {
    }
}
