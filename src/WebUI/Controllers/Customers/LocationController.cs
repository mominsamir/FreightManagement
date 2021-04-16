
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Commands.AddTankToLocation;
using FreightManagement.Application.Customers.Commands.CreateLocation;
using FreightManagement.Application.Customers.Commands.RemoveTankFromLocation;
using FreightManagement.Application.Customers.Commands.UpdateLocation;
using FreightManagement.Application.Customers.Commands.UpdateLocationStatus;
using FreightManagement.Application.Customers.Queries.GetLocationById;
using FreightManagement.Application.Customers.Queries.SearchLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Customers
{
    public class LocationController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<LocationDto>>> GetLocaton(long id)
        {
            return await Mediator.Send(new QueryLocationById(id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<LocationDto>>> Search(QueryLocationSearch query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> Update(int id, UpdateLocationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("location not found");
            }

            await Mediator.Send(command);

            return Ok(new { Id = id, success = true, message = "Location is updated." });

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> Create(CreateLocationCommand command)
        {
            var id =  await Mediator.Send(command);
            return new { Id = id, sucess = true, message="Location Created." };
        }

        [HttpPost]
        [Route("tank")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> AddTank(AddTankToLocationCommand command)
        {
            await Mediator.Send(command);
            return new { Id = command.Id, sucess = true, message = "Tank Added." };
        }


        [HttpDelete]
        [Route("{id}/tank/{tankId}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> RemoveTank(long id, long tankId)
        {
            await Mediator.Send( new RemoveTankFromLocationCammand(id, tankId) );
            return new { Id = id, sucess = true, message = "Tank Removed." };
        }

        [HttpPut]
        [Route("{id}/activate")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> ActivateLocation(long id)
        {
            await Mediator.Send(new UpdateLocationStatusCommand(id,true));
            return new { Id = id, sucess = true, message = "Location activated." };
        }

        [HttpPut]
        [Route("{id}/deactivate")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> DeactivateLocation(long id)
        {
            await Mediator.Send(new UpdateLocationStatusCommand(id,false));
            return new { Id = id, sucess = true, message = "Location deactivated." };
        }

    }
}
