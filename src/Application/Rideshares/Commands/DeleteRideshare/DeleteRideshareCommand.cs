using MediatR;

namespace Application.Rideshares.Commands.DeleteRideshare
{
    public class DeleteRideshareCommand : IRequest
    {
        public int Id { get; set; }
    }
}
