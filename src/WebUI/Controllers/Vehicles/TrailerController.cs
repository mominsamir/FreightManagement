using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trailers.Commands.CreateTrailer;
using FreightManagement.Application.Trailers.Commands.UpdateTrailer;
using FreightManagement.Application.Trailers.Commands.UpdateTrailerStatus;
using FreightManagement.Application.Trailers.Queries.GetRacks;
using FreightManagement.Application.Trailers.Queries.TrailerSearch;
using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Vehicles
{
    [Authorize]
    public class TrailerController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "DRIVER,ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<TrailerDto>>> GetRack(long id)
        {
            return await Mediator.Send(new GetTrailerById { Id = id });
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "DRIVER,ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<TrailerListDto>>> Search(QueryTrailerSearch query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<long>> Create(CreateTrailerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> Update(int id, UpdateTrailerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> ActivateTerminal(int id)
        {
            var command = new UpdateTrailerStatusCommand
            {
                Id = id,
                status = VehicleStatus.ACTIVE
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/under_maintanace")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> UnderMaintanance(int id)
        {
            var command = new UpdateTrailerStatusCommand
            {
                Id = id,
                status = VehicleStatus.UNDER_MAINTNCE
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/out_of_service")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> OutOfService(int id)
        {
            var command = new UpdateTrailerStatusCommand
            {
                Id = id,
                status = VehicleStatus.OUT_OF_SERVICE
            };
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
