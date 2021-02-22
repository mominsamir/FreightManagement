using FreightManagement.Application.StorageRacks.Queries.GetRacks;
using FreightManagement.Application.Terminal.Commands.ActivateTerminal;
using FreightManagement.Application.Terminal.Commands.CreateTerminal;
using FreightManagement.Application.Terminal.Commands.UpdateTerminal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Terminal
{
    [Authorize]
    public class RackController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<RackDto>> GetRack(long id)
        {
            return await Mediator.Send(new GetRackById { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateRackCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateRackCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<ActionResult> ActivateTerminal(int id)
        {
            var command = new RackStatusCommand
            {
                Id = id,
                IsActive = true
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/inactivate")]
        public async Task<ActionResult> deactivateTerminal(int id)
        {
            var command = new RackStatusCommand
            {
                Id = id,
                IsActive = false
            };
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
