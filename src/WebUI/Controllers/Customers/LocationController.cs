
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Commands.CreateLocation;
using FreightManagement.Application.Customers.Queries.GetLocationById;
using FreightManagement.Application.Customers.Queries.SearchLocation;
using MediatR;
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


        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<long>> Create(CreateLocationCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
