using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Products.Commands.CreateFuelProduct;
using FreightManagement.Application.Products.Commands.UpdateFuelProduct;
using FreightManagement.Application.Products.Queries.GetFuelProduct;
using FreightManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Products
{
    [Authorize(Roles = "manager")]
    public class FuelProductController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = Role.DRIVER)]
        [Authorize(Roles = Role.ADMIN)]
        [Authorize(Roles = Role.DISPATCHER)]
        public async Task<ActionResult<ModelView<FuelProductDto>>> GetRack(long id)
        {
            return await Mediator.Send(new GetFuelProductById ( id ));
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        [Authorize(Roles = Role.DISPATCHER)]
        public async Task<ActionResult<long>> Create(CreateFuelProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        [Authorize(Roles = Role.DISPATCHER)]
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
