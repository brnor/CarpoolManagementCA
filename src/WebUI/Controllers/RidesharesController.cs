using Application.Rideshares.Commands.CreateRideshare;
using Application.Rideshares.Commands.DeleteRideshare;
using Application.Rideshares.Commands.UpdateRideshare;
using Application.Rideshares.Queries.GetRideshareDetail;
using Application.Rideshares.Queries.GetRidesharesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RidesharesController : ControllerBase
    {
        private readonly IMediator Mediator;
        public RidesharesController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vm = await Mediator.Send(new GetRidesharesListQuery());

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new GetRideshareDetailQuery { Id = id });

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRideshareCommand command)
        {
            var rideshareId = await Mediator.Send(command);

            return Ok(rideshareId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRideshareCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRideshareCommand { Id = id });

            return NoContent();
        }
    }
}
