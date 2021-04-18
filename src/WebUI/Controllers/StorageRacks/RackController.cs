using FreightManagement.Application.Common.Models;
using FreightManagement.Application.StorageRacks.Queries.GetRacks;
using FreightManagement.Application.StorageRacks.Queries.RackSearch;
using FreightManagement.Application.Terminal.Commands.ActivateTerminal;
using FreightManagement.Application.Terminal.Commands.CreateTerminal;
using FreightManagement.Application.Terminal.Commands.UpdateTerminal;
using FreightManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Terminal
{

    public class RackController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<RackDto>>> GetRack(long id)
        {
            return await Mediator.Send(new GetRackById (id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<PaginatedList<RackDto>>> Search(QueryRackSearch search)
        {
            return await Mediator.Send(search);
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Create(CreateRackCommand command)
        {
            var id = await Mediator.Send(command);
            return new { Id = id, sucess = true, message = "Rack Added." };
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Update(int id, UpdateRackCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "Rack Updated." };
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> ActivateTerminal(int id)
        {
            var command = new RackStatusCommand
            {
                Id = id,
                IsActive = true
            };
            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "Rack activated." };
        }

        [HttpPut("{id}/inactivate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> DeactivateTerminal(int id)
        {
            var command = new RackStatusCommand
            {
                Id = id,
                IsActive = false
            };
            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "Rack inactivated." };
        }

    }
}
