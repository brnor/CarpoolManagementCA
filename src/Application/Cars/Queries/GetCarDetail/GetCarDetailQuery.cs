using MediatR;
using System;

namespace Application.Cars.Queries.GetCarDetail
{
    public class GetCarDetailQuery : IRequest<CarDetailVm>
    {
        public int Id { get; set; }
    }
}
