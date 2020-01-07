using MediatR;
using System;

namespace Application.Rideshares.Queries.GetRideshareDetail
{
    public class GetRideshareDetailQuery : IRequest<RideshareDetailVm>
    {
        public int Id { get; set; }
    }
}
