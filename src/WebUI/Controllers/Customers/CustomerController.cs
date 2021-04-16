using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Commands.AddLocationToCustomer;
using FreightManagement.Application.Customers.Commands.CreateCustomer;
using FreightManagement.Application.Customers.Commands.RemoveLocationCustomer;
using FreightManagement.Application.Customers.Commands.UpdateCustomerStatus;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
using FreightManagement.Application.Customers.Queries.GetLocationByCustomerId;
using FreightManagement.Application.Customers.Queries.SearchCustomer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Customers
{
    public class CustomerController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<CustomerDto>>> GetCustomer(long id)
        {
            return await Mediator.Send(new QueryCustomerById(id));
        }

        [HttpGet("{id}/locations")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<CustomerDto>> GetCustomerLocation(long id)
        {
            return await Mediator.Send(new QueryLocationByCustomerId(id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> Search(QueryCustomerSearch query)
        {
            return await Mediator.Send(query);
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<long>> Create(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("location")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> AddLocation(AddLocationToCustomerCommand command)
        {
            await Mediator.Send(command);
            return new { Id = command.Id, sucess = true, message = "Location Added." };
        }


        [HttpDelete]
        [Route("{id}/location/{tankId}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> RemoveLocation(long id, long tankId)
        {
            await Mediator.Send(new RemoveLocationFromCustomerCommand(id, tankId));
            return new { Id = id, sucess = true, message = "Location Removed." };
        }

        [HttpPut]
        [Route("{id}/activate")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> ActivateLocation(long id)
        {
            await Mediator.Send(new UpdateCustomerStatusCommand(id, true));
            return new { Id = id, sucess = true, message = "Location activated." };
        }

        [HttpPut]
        [Route("{id}/deactivate")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<dynamic>> DeactivateLocation(long id)
        {
            await Mediator.Send(new UpdateCustomerStatusCommand(id, false));
            return new { Id = id, sucess = true, message = "Location deactivated." };
        }
    }
}
