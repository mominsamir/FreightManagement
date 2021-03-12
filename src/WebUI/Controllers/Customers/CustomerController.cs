using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Commands.CreateCustomer;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
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
    }
}
