using FreightManagement.Application.Products.Commands.CreateProduct;
using FreightManagement.Application.Products.Commands.UpdateProduct;
using FreightManagement.Application.Products.Queries.GetProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Products
{
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetRack(long id)
        {
            return await Mediator.Send(new GetProductById { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductCommand command)
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
