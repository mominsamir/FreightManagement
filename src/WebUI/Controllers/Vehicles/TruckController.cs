using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trucks.Commands.CreateTruck;
using FreightManagement.Application.Trucks.Commands.UpdateTruck;
using FreightManagement.Application.Trucks.Commands.UpdateTruckStatus;
using FreightManagement.Application.Trucks.Queries;
using FreightManagement.Application.Trucks.Queries.TruckSearch;
using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Vehicles
{
    [Authorize]
    public class TruckController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "DRIVER,ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<TruckDto>>> GetRack(long id)
        {
            return await Mediator.Send(new GetTruckById (id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "DRIVER,ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<TruckListDto>>> Search(QueryTruckSearch query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<long>> Create(CreateTruckCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> Update(int id, UpdateTruckCommand command)
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
            var command = new UpdateTruckStatusCommand
            {
                Id = id,
                status = VehicleStatus.ACTIVE
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/out_of_service")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> OutOfService(int id)
        {
            var command = new UpdateTruckStatusCommand
            {
                Id = id,
                status = VehicleStatus.OUT_OF_SERVICE
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/under_maintanace")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> UnderMaintance(int id)
        {
            var command = new UpdateTruckStatusCommand
            {
                Id = id,
                status = VehicleStatus.UNDER_MAINTNCE
            };
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
