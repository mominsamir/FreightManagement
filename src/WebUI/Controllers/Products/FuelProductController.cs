using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Products.Commands.CreateFuelProduct;
using FreightManagement.Application.Products.Commands.UpdateFuelProduct;
using FreightManagement.Application.Products.Queries.GetFuelProduct;
using FreightManagement.Application.Products.Queries.ProductSearch;
using FreightManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Products
{

    public class FuelProductController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<ModelView<FuelProductDto>>> GetRack(long id)
        {
            return await Mediator.Send(new GetFuelProductById ( id ));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<PaginatedList<FuelProductListDto>>> Search(QueryFuelProductSearch search)
        {
            return await Mediator.Send(search);
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<long>> Create(CreateFuelProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult> Update(int id, UpdateFuelProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }

}
